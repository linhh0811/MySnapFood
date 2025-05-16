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
        public async Task<bool> ApproveAsync(Guid id)//Duyệt
        {
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product is not null) 
            { 
                product.ModerationStatus= ModerationStatus.Approved;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RejectAsync(Guid id) //Hủy duyệt
        {
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product is not null)
            {
                product.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.ProductRepo.Update(product);
                await _unitOfWork.CompleteAsync();
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

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
            return product;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            int totalRecords = 0;
            var dataQuery = _unitOfWork.ProductRepo.FilterData(
                q => q, // Bỏ hoàn toàn điều kiện Where
                query.gridRequest,
                ref totalRecords
            );
            var data = dataQuery.AsEnumerable()
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
                CreatedBy = m.CreatedBy,
                LastModifiedBy = m.LastModifiedBy,
            });

           
            DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
            dataTableJson.querytext = dataQuery.ToString();
            return dataTableJson;
        }
        #endregion

        #region thêm, sửa, xóa
        public async Task<Guid> CreateAsync(ProductDto item)
        {
            try
            {
                if (item.Image != null && item.Image.Length > 0)
                {
                    var imageUrl = SaveImage(item.Image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        item.ImageUrl = imageUrl;
                    }
                }
                else
                {
                    throw new Exception("Ảnh không hợp lệ");
                }
                Product product = new Product
                {
                    CategoryId = item.CategoryId,
                    SizeId = item.SizeId,
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
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(id);
                if (product is null)
                {
                    throw new Exception("Không tìm thấy sản phẩm");
                }
                if (item.Image != null && item.Image.Length > 0)
                {
                    var imageUrl = SaveImage(item.Image);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        item.ImageUrl = imageUrl;
                    }
                }
                else
                {
                    throw new Exception("Ảnh không hợp lệ");
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
            catch (Exception)
            {

                throw;
            }
           

        }
        #endregion
        private string? SaveImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var dataProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Service.SnapFood.Share", "Images");

                if (!Directory.Exists(dataProjectPath))
                {
                    Directory.CreateDirectory(dataProjectPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(dataProjectPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return $"/images/{fileName}";
            }
            return null;
        }
    }
}
