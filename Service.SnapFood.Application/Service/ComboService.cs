using Microsoft.EntityFrameworkCore;
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
    internal class ComboService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComboService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region Duyệt, hủy duyệt
        public async Task<bool> ApproveAsync(Guid id)
        {
            var combo = _unitOfWork.ComboRepo.GetById(id);
            if (combo is not null)
            {
                combo.ModerationStatus = ModerationStatus.Approved;
                _unitOfWork.ComboRepo.Update(combo);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RejectAsync(Guid id)
        {
            var combo = _unitOfWork.ComboRepo.GetById(id);
            if (combo is not null)
            {
                combo.ModerationStatus = ModerationStatus.Rejected;
                _unitOfWork.ComboRepo.Update(combo);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        #endregion
        #region Get dữ liệu
        public async Task<List<Combo>> GetAllAsync()
        {
            var combo = await _unitOfWork.ComboRepo.GetAllAsync();
            return combo.ToList();
        }

        public async Task<ComboDto> GetByIdAsync(Guid id)
        {
            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);

            if (combo is not null)
            {
                // Get combo items
                var comboItems = _unitOfWork.ProductComboRepo
                    .FindWhere(x => x.ComboId == id)
                    .Select(x => new ComboProductDto
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId).ProductName
                    })
                    .ToList();

                // Get category name
                var category = _unitOfWork.CategoriesRepo.GetById(combo.CategoryId);

                var comboDto = new ComboDto
                {
                    Id = combo.Id,
                    CategoryId = combo.CategoryId,
                    CategoryName = category.CategoryName,
                    ComboName = combo.ComboName,
                    ImageUrl = combo.ImageUrl,
                    BasePrice = combo.BasePrice,
                    Description = combo.Description,
                    Quantity = combo.Quantity,
                    Created = combo.Created,
                    LastModified = combo.LastModified,
                    ModerationStatus = combo.ModerationStatus,
                    CreatedBy = combo.CreatedBy,
                    LastModifiedBy = combo.LastModifiedBy,
                    ComboItems = comboItems
                };
                return comboDto;
            }
            return null;
        }

        public DataTableJson GetPaged(BaseQuery query)
        {
            int totalRecords = 0;
            var dataQuery = _unitOfWork.ComboRepo.FilterData(
                q => q, // Bỏ hoàn toàn điều kiện Where
                query.gridRequest,
                ref totalRecords
            ).Include(x=>x.Category);

            // Lấy danh sách ID combo để tối ưu truy vấn
            var comboIds = dataQuery.Select(m => m.Id).ToList();

            // Lấy tất cả combo items một lần duy nhất
            var allComboItems = _unitOfWork.ProductComboRepo
                .FindWhere(x => comboIds.Contains(x.ComboId))
                .GroupBy(x => x.ComboId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => new ComboProductDto
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId).ProductName
                    }).ToList()
                );

            var data = dataQuery.AsEnumerable()
                .Select((m, i) => new ComboDto
                {
                    Id = m.Id,
                    Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category.CategoryName,
                    ComboName = m.ComboName,
                    ImageUrl = m.ImageUrl,
                    BasePrice = m.BasePrice,
                    Description = m.Description,
                    Quantity = m.Quantity,
                    Created = m.Created,
                    LastModified = m.LastModified,
                    ModerationStatus = m.ModerationStatus,
                    CreatedBy = m.CreatedBy,
                    LastModifiedBy = m.LastModifiedBy,
                    ComboItems = allComboItems.TryGetValue(m.Id, out var items) ? items : new List<ComboProductDto>()
                });

            DataTableJson dataTableJson = new DataTableJson(data, query.draw, totalRecords);
            dataTableJson.querytext = dataQuery.ToString();
            return dataTableJson;
        }
      
        #endregion
        #region Thêm, sửa, xóa
        public async Task<Guid> CreateAsync(ComboDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                if (item.CategoryId == Guid.Empty)
                {
                    throw new Exception("Phân loại trống");
                }
                if (string.IsNullOrEmpty(item.ComboName))
                {
                    throw new Exception("Tên combo trống");
                }
                if (string.IsNullOrEmpty(item.ImageUrl))
                {
                    throw new Exception("Ảnh trống");
                }
                if (item.Quantity<=0)
                {
                    throw new Exception("Giá nhỏ hơn 0");
                }
                if (item.ComboItems is null || !item.ComboItems.Any())
                {
                    throw new Exception("Sản phẩm trống");
                }
                Combo combo = new Combo
                {
                    CategoryId = item.CategoryId,
                    ComboName = item.ComboName,
                    ImageUrl = item.ImageUrl,
                    BasePrice = item.BasePrice,
                    Description = item.Description,
                };
                _unitOfWork.ComboRepo.Add(combo);
                await _unitOfWork.CompleteAsync();
                if (item.ComboItems != null && item.ComboItems.Any())
                {
                    foreach (var comboItem in item.ComboItems)
                    {
                        var productCombo = new ProductCombo
                        {
                            ComboId = combo.Id,
                            ProductId = comboItem.ProductId,
                            Quantity = comboItem.Quantity,
                        };

                        _unitOfWork.ProductComboRepo.Add(productCombo);
                    }

                    await _unitOfWork.CompleteAsync();
                }
                await _unitOfWork.CommitAsync();
                return combo.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi tạo combo: " + ex.Message);
            }
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);
            if (combo is null)
            {
                throw new Exception("Không tìm thấy combo");
            }
            _unitOfWork.ComboRepo.Delete(combo);
            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(Guid id, ComboDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);
                if (combo is null)
                {
                    throw new Exception("Không tìm thấy combo");
                }
                if (item.CategoryId == Guid.Empty)
                {
                    throw new Exception("Phân loại trống");
                }
                if (string.IsNullOrEmpty(item.ComboName))
                {
                    throw new Exception("Tên combo trống");
                }
                if (string.IsNullOrEmpty(item.ImageUrl))
                {
                    throw new Exception("Ảnh trống");
                }
                if (item.BasePrice <= 0)
                {
                    throw new Exception("Giá nhỏ hơn 0");
                }
                if (item.ComboItems is null || !item.ComboItems.Any())
                {
                    throw new Exception("Sản phẩm trống");
                }

                combo.CategoryId = item.CategoryId;
                combo.ComboName = item.ComboName;
                combo.ImageUrl = item.ImageUrl;
                combo.BasePrice = item.BasePrice;
                combo.Description = item.Description;

                _unitOfWork.ComboRepo.Update(combo);
                await _unitOfWork.CompleteAsync();

                var productComboDelete = _unitOfWork.ProductComboRepo.FindWhere(x => x.ComboId == id);
                _unitOfWork.ProductComboRepo.DeleteRange(productComboDelete);
                await _unitOfWork.CompleteAsync();

                if (item.ComboItems != null && item.ComboItems.Any())
                {
                    foreach (var comboItem in item.ComboItems)
                    {
                        if (comboItem.Quantity<=0)
                        {
                            throw new Exception($"Số lượng của sản phẩm: {comboItem.ProductName} nhỏ hơn 0");
                        }
                        var productCombo = new ProductCombo
                        {
                            ComboId = combo.Id,
                            ProductId = comboItem.ProductId,
                            Quantity = comboItem.Quantity,
                        };

                        _unitOfWork.ProductComboRepo.Add(productCombo);
                    }
                    await _unitOfWork.CompleteAsync();

                }
                await _unitOfWork.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }

        }
        #endregion
    }
}
