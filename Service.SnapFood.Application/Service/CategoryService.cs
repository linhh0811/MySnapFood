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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region thêm, sửa, xóa
        public async Task<Guid> CreateAsync(CategoryDto item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.CategoryName))
                {
                    throw new Exception("Tên phân loại không được để trống");
                }
                Categories category = new Categories
                {
                    CategoryName = item.CategoryName,
                    ImageUrl=item.ImageUrl,
                    DisplayOrder = item.DisplayOrder,

                };
                _unitOfWork.CategoriesRepo.Add(category);
                await _unitOfWork.CommitAsync();
                return category.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> UpdateAsync(Guid id, CategoryDto item)
        {
            try
            {
                var category = await _unitOfWork.CategoriesRepo.GetByIdAsync(id);
                if (category is null)
                {
                    throw new Exception("Không tìm thấy phân loại");
                }
                if (string.IsNullOrEmpty(item.CategoryName))
                {
                    throw new Exception("Tên phân loại không được để trống");
                }


                category.CategoryName = item.CategoryName;
                category.ImageUrl = item.ImageUrl;
                category.DisplayOrder = item.DisplayOrder;
                _unitOfWork.CategoriesRepo.Update(category);
                await _unitOfWork.CompleteAsync();
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _unitOfWork.CategoriesRepo.GetByIdAsync(id);
            if (product is null)
            {
                throw new Exception("Không tìm thấy phân loại");
            }
            _unitOfWork.CategoriesRepo.Delete(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        #endregion
        #region Get dữ liệu

        public async Task<List<Categories>> GetAllAsync()
        {
            var categories =await _unitOfWork.CategoriesRepo.GetAllAsync();
            return categories.ToList();
        }

        public async Task<Categories?> GetByIdAsync(Guid id)
        {
            var category =await _unitOfWork.CategoriesRepo.GetByIdAsync(id);
            return category;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            var productCategoryIds = _unitOfWork.ProductRepo
                                    .GetAll()
                                    .Select(p => p.CategoryId)
                                    .Distinct();
            var comboCategoryIds = _unitOfWork.ComboRepo
                                    .GetAll()
                                    .Select(p => p.CategoryId)
                                    .Distinct();
            int totalRecords = 0;
            var dataQuery = _unitOfWork.CategoriesRepo.FilterData(
                q => q, // Bỏ hoàn toàn điều kiện Where
                query.gridRequest,
                ref totalRecords
            );
            var data = dataQuery.AsEnumerable()
            .Select((m, i) => new CategoryDto
            {
                Id = m.Id,
                Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                CategoryName = m.CategoryName,
                ImageUrl = m.ImageUrl,
                DisplayOrder = m.DisplayOrder,
                Created = m.Created,
                LastModified = m.LastModified,
                ModerationStatus = m.ModerationStatus,
                CreatedBy = m.CreatedBy,
                LastModifiedBy = m.LastModifiedBy,
                IsExits= productCategoryIds.Contains(m.Id) || comboCategoryIds.Contains(m.Id)
            });


            DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
            dataTableJson.querytext = dataQuery.ToString();
            return dataTableJson;
        }
        #endregion


        


        #region Duyệt, hủy duyệt
        public async Task<bool> ApproveAsync(Guid id)//Duyệt
        {
            var product = _unitOfWork.CategoriesRepo.GetById(id);
            if (product is not null)
            {
                product.ModerationStatus = ModerationStatus.Approved;
                _unitOfWork.CategoriesRepo.Update(product);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }

        public int CheckRejectAsync(Guid id) //Hủy duyệt
        {
            var ProductApprovedCount = _unitOfWork.ProductRepo
                .FindWhere(x => x.CategoryId == id && x.ModerationStatus == ModerationStatus.Approved)
                .Select(x => x.Id)
                .Distinct()
                .Count();

            var ComboApprovedCount = _unitOfWork.ComboRepo
                .FindWhere(x => x.CategoryId == id && x.ModerationStatus == ModerationStatus.Approved)
                .Select(x => x.Id)
                .Distinct()
                .Count();



            return ComboApprovedCount+ ProductApprovedCount;
        }
        public async Task<bool> RejectAsync(Guid id) //Hủy duyệt
        {
            var category = _unitOfWork.CategoriesRepo.GetById(id);
            if (category is not null)
            {
                category.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.CategoriesRepo.Update(category);
                await _unitOfWork.CompleteAsync();
                var comboIds = _unitOfWork.ComboRepo
                    .FindWhere(x => x.CategoryId == id && x.ModerationStatus == ModerationStatus.Approved)
                    .Select(x => x.Id);      

                if (comboIds.Count() > 0)
                {
                    foreach (var comboId in comboIds)
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
                var productIds = _unitOfWork.ProductRepo
                    .FindWhere(x => x.CategoryId == id && x.ModerationStatus == ModerationStatus.Approved)
                    .Select(x => x.Id);

                if (productIds.Count() > 0)
                {
                    foreach (var productId in productIds)
                    {
                        var product = _unitOfWork.ProductRepo.GetById(productId);
                        if (product is not null)
                        {
                            product.ModerationStatus = ModerationStatus.Rejected;
                            _unitOfWork.ProductRepo.Update(product);
                            await _unitOfWork.CompleteAsync();
                        }

                    }
                }
                return true;

            }
            return false;
        }

        #endregion
    }
}
