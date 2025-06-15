using Microsoft.AspNetCore.Http;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Service.SnapFood.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Duyệt, hủy duyệt
        public Dtos.StringContent CheckApprove(Guid id) // duyệt
        {
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product is not null)
            {
                Dtos.StringContent stringContent = new Dtos.StringContent();
                string CheckApproveCategory = string.Empty;
                var category = _unitOfWork.CategoriesRepo.GetById(product.CategoryId);
                if (category is not null && category.ModerationStatus== ModerationStatus.Rejected)
                {
                    CheckApproveCategory = "Phân loại";
                }
                string CheckApproveSize = string.Empty;
                var size = _unitOfWork.SizesRepo.GetById(product.SizeId??Guid.Empty);
                if (size is not null && size.ModerationStatus == ModerationStatus.Rejected)
                {
                    CheckApproveSize = "Size";
                }
                if (!string.IsNullOrEmpty(CheckApproveCategory)&&!string.IsNullOrEmpty(CheckApproveSize) )
                {
                    stringContent.Content= $"{CheckApproveCategory} và {CheckApproveSize}";
                }
                if (string.IsNullOrEmpty(CheckApproveCategory) && !string.IsNullOrEmpty(CheckApproveSize))
                {
                    stringContent.Content= CheckApproveSize;
                }
                if (!string.IsNullOrEmpty(CheckApproveCategory) && string.IsNullOrEmpty(CheckApproveSize))
                {
                    stringContent.Content = CheckApproveCategory;
                }
                return stringContent;
            }

            return new Dtos.StringContent();


        }
        public async Task<bool> ApproveAsync(Guid id)//Duyệt
        {
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product is not null) 
            { 
                product.ModerationStatus= ModerationStatus.Approved;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.CompleteAsync();
                var category = _unitOfWork.CategoriesRepo.GetById(product.CategoryId);
                if (category is not null && category.ModerationStatus == ModerationStatus.Rejected)
                {
                    category.ModerationStatus = ModerationStatus.Approved;
                    await _unitOfWork.CompleteAsync();

                }
                var size = _unitOfWork.SizesRepo.GetById(product.SizeId ?? Guid.Empty);
                if (size is not null && size.ModerationStatus == ModerationStatus.Rejected)
                {
                    size.ModerationStatus = ModerationStatus.Approved;
                    await _unitOfWork.CompleteAsync();

                }
                return true;
            }
            return false;
        }

        public int CheckRejectAsync(Guid id) //Hủy duyệt
        {
            var comboIds = _unitOfWork.ProductComboRepo
                .FindWhere(x => x.ProductId == id)
                .Select(x => x.ComboId)
                .Distinct();

            var ComboApprovedCount = _unitOfWork.ComboRepo
                .FindWhere(x => comboIds.Contains(x.Id) && x.ModerationStatus== ModerationStatus.Approved) 
                .Count();



            return ComboApprovedCount;
        }

        public async Task<bool> RejectAsync(Guid id) //Hủy duyệt
        {
           
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product is not null)
            {
                product.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.CompleteAsync();
                var comboIds = _unitOfWork.ProductComboRepo.FindWhere(x => x.ProductId == id).Select(x => x.ComboId);
                var ComboApprovedIds = _unitOfWork.ComboRepo
                   .FindWhere(x => comboIds.Contains(x.Id) && x.ModerationStatus == ModerationStatus.Approved)
                   .Select(x => x.Id);
                if (ComboApprovedIds.Count() > 0)
                {
                    foreach (var comboId in ComboApprovedIds)
                    {
                        var combo = _unitOfWork.ComboRepo.GetById(comboId);
                        if (combo is not null)
                        {
                            combo.ModerationStatus = ModerationStatus.Rejected;
                            _unitOfWork.ComboRepo.Update(combo);
                            await _unitOfWork.CompleteAsync();
                        }

                    }
                }
                return true;
            }
           
           
            return false;
        }

        #endregion


        #region Get dữ liệu
        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepo.GetAllAsync();
            return products.ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            if (product is not null)
            {
                var category = _unitOfWork.CategoriesRepo.GetById(product.CategoryId);
                ProductDto productDto = new ProductDto()
                {
                    Id = product.Id,
                    CategoryId = product.CategoryId,
                    CategoryName = category?.CategoryName ?? string.Empty,
                    SizeId = product.SizeId,
                    ImageUrl = product.ImageUrl,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    BasePrice = product.BasePrice,
                    ModerationStatus = product.ModerationStatus
                };
                if (product.SizeId is not null)
                {
                    var sizes = _unitOfWork.SizesRepo.FindWhere(x => x.ParentId == product.SizeId && x.ModerationStatus == ModerationStatus.Approved);
                    var sizeDtos = sizes.Select(x => new SizeDto()
                    {
                        Id = x.Id,
                        SizeName = x.SizeName,
                        AdditionalPrice = x.AdditionalPrice,
                        DisplayOrder = x.DisplayOrder,
                    }).OrderBy(x => x.DisplayOrder);

                    productDto.Sizes = sizeDtos.ToList();
                }

                return productDto;
            }
            return null;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            try
            {
                int totalRecords = 0;
                var dataQuery = _unitOfWork.ProductRepo.FilterData(
                    q => q, // Bỏ hoàn toàn điều kiện Where
                    query.gridRequest,
                    ref totalRecords
                );
                var data = dataQuery.ToList()
                .Select((m, i) => new ProductDto
                {
                    Id = m.Id,
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    CategoryId = m.CategoryId,
                    SizeId = m.SizeId,
                    ImageUrl = m.ImageUrl,
                    ProductName = m.ProductName,
                    Description = m.Description,
                    Quantity = m.Quantity,
                    BasePrice = m.BasePrice,
                    Created = m.Created,
                    LastModified = m.LastModified,
                    ModerationStatus = m.ModerationStatus,
                    CategoryModerationStatus = GetCategoryModerationStatusById(m.CategoryId),
                    SizeModerationStatus=GetSizeModerationStatusById(m.SizeId??Guid.Empty),
                    CreatedBy = m.CreatedBy,
                    LastModifiedBy = m.LastModifiedBy,
                    SizeName = GetSizeNameById(m.SizeId ?? Guid.Empty) ?? null,
                    CategoryName = GetCategoryNameById(m.CategoryId),
                });


                DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
                dataTableJson.querytext = dataQuery.ToString();
                return dataTableJson;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            
        }
        #endregion

        #region thêm, sửa, xóa
        public async Task<Guid> CreateAsync(ProductDto item)
        {
            try
            {
                if (item is null) throw new Exception("Sản phẩm trống");

                if (string.IsNullOrEmpty(item.ProductName))
                {
                    throw new Exception("Tên sản phẩm trống");
                }

                if (item.BasePrice <=0)
                {
                    throw new Exception("Giá sản phẩm nhỏ hơn 0");
                }

                if (string.IsNullOrEmpty(item.ImageUrl))
                {
                    throw new Exception("Ảnh sản phẩm trống");
                }


                Product product = new Product
                {
                    CategoryId = item.CategoryId,
                    SizeId = item.SizeId ,
                    ImageUrl = item.ImageUrl,
                    ProductName = item.ProductName,
                    Description = item.Description,
                    Quantity = 0,
                    BasePrice = item.BasePrice,
                };
                _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.CommitAsync();
                return product.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product =await _unitOfWork.ProductRepo.GetByIdAsync(id);
            if (product is null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            _unitOfWork.ProductRepo.Delete(product);
            await _unitOfWork.CompleteAsync();
            return true;

        }
        public async Task<bool> UpdateAsync(Guid id, ProductDto item)
        {
            try
            {
                if (item is null) throw new Exception("Sản phẩm trống");

                if (item.ProductName is null)
                {
                    throw new Exception("Tên sản phẩm trống");
                }

                if (item.BasePrice <= 0)
                {
                    throw new Exception("Giá sản phẩm nhỏ hơn 0");
                }

                if (item.ImageUrl is null)
                {
                    throw new Exception("Ảnh sản phẩm trống");
                }

                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if (product is null)
                {
                    throw new Exception("Không tìm thấy sản phẩm");
                }
               
                product.CategoryId= item.CategoryId;
                product.SizeId= item.SizeId;
                product.ProductName= item.ProductName;
                product.ImageUrl= item.ImageUrl;
                product.Description= item.Description;
                product.BasePrice= item.BasePrice;

                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.CompleteAsync();
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message) ;
            }
           

        }
        #endregion
        private ModerationStatus GetCategoryModerationStatusById(Guid id)
        {

            var category = _unitOfWork.CategoriesRepo.GetById(id);
            if (category is null)
            {
                return 0;
            }
            else
            {
                return category.ModerationStatus;
            }
        }
        private ModerationStatus GetSizeModerationStatusById(Guid id)
        {

            var category = _unitOfWork.SizesRepo.GetById(id);
            if (category is null)
            {
                return 0;
            }
            else
            {
                return category.ModerationStatus;
            }
        }
        private string GetCategoryNameById(Guid id)
        {

            var category = _unitOfWork.CategoriesRepo.GetById(id);
            if (category is null)
            {
                return string.Empty;
            }
            else
            {
                return category.CategoryName;
            }
        }
        private string? GetSizeNameById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var sizes = _unitOfWork.SizesRepo.GetById(id);
            if (sizes is not null)
            {
                var sizeChild = _unitOfWork.SizesRepo.FindWhere(x => x.ParentId == sizes.Id && x.ModerationStatus == ModerationStatus.Approved).OrderBy(x=>x.DisplayOrder);
                var nameSize = sizes.SizeName + "(" + string.Join(", ", sizeChild.Select(x => x.SizeName)) + ")";
                return nameSize;
            }
            else
            {
                return null;
            }
            
        }
    }
}
