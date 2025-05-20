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

         public async Task<List<Combo>> GetAllAsync()
        {
            var combo = await _unitOfWork.ComboRepo.GetAllAsync();
            return combo.ToList();
        }

        public async Task<Combo> GetByIdAsync(Guid id)
        {
            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);
            return combo;
        }

         public DataTableJson GetPaged(BaseQuery query)
        {
            int totalRecords = 0;
            var dataQuery = _unitOfWork.ComboRepo.FilterData(
                q => q, // Bỏ hoàn toàn điều kiện Where
                query.gridRequest,
                ref totalRecords
            );
            var data = dataQuery.AsEnumerable()
            .Select((m, i) => new ComboDto
            {
                Id = m.Id,
                Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                CategoryId = m.CategoryId,
                ComboName = m.ComboName,
                ImageUrl = m.ImageUrl,
                BasePrice = m.BasePrice,
                Description = m.Description,
                CreteDate = m.CreteDate,
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


        public async Task<Guid> CreateAsync(ComboDto item)
        {
            try
            {
                
                Combo combo = new Combo
                {
                    CategoryId = item.CategoryId,
                    ComboName = item.ComboName,
                    ImageUrl = item.ImageUrl,
                    BasePrice = item.BasePrice,
                    Description = item.Description,
                    CreteDate = item.CreteDate
                };
                if (item.Products != null)
                {
                    
                foreach (var comboProductDto in item.Products)
                {
                    combo.ProductComboes.Add(new ProductCombo
                    {
                        ProductId = comboProductDto.ProductId,
                        Quantity = comboProductDto.Quantity,
                        Combo = combo 
                    });
                }
                }


               

                _unitOfWork.ComboRepo.Add(combo);
                await _unitOfWork.CommitAsync();

                return combo.Id;
            }
            catch (Exception ex)
            {
                var innerMsg = ex.InnerException?.Message ?? "";
                throw new Exception("Lỗi khi tạo combo: " + ex.Message + " | Inner: " + innerMsg);
            }
        }



        public async Task<bool> DeleteAsync(Guid id)
        {
            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);
            if (combo is null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            _unitOfWork.ComboRepo.Delete(combo);
            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> UpdateAsync(Guid id, ComboDto item)
        {
            try
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(id);
                if (combo is null)
                {
                    throw new Exception("Không tìm thấy sản phẩm");
                }
              
                combo.CategoryId = item.CategoryId;
                combo.ComboName = item.ComboName;
                combo.ImageUrl = item.ImageUrl;
                combo.BasePrice = item.BasePrice;
                combo.Description = item.Description;
                combo.CreteDate = item.CreteDate;

                _unitOfWork.ComboRepo.Update(combo);
                await _unitOfWork.CompleteAsync();
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
