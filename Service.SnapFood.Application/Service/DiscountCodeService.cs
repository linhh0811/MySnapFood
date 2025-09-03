


using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Application.Service
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountCodeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DataTableJson GetPaged(VoucherQuery query)
        {
            int totalRecords = 0;

            var dataQuery = _unitOfWork.DiscountCodeRepo.FilterData(
                q => q.Where(x => query.ModerationStatus == ModerationStatus.None ? true : x.ModerationStatus == query.ModerationStatus)
                .Where(x=>query.IsActive? x.ModerationStatus == ModerationStatus.Approved && x.EndDate >= DateTime.Now:true),
                query.gridRequest,
                ref totalRecords
            );

            var data = dataQuery.ToList().Select((m, i) => new DiscountCodeDto
            {
                Id = m.Id,
                Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                Code = m.Code,
                Description = m.Description,
                DiscountValue = m.DiscountValue,
                DiscountValueMax = m.DiscountValueMax,

                StartDate = m.StartDate,
                EndDate = m.EndDate,
                UsageLimit = m.UsageLimit,
                UsedCount = m.UsedCount,
                MinOrderAmount = m.MinOrderAmount,
                ModerationStatus = m.ModerationStatus,
                Created = m.Created,
                LastModified = m.LastModified,
                CreatedBy = m.CreatedBy,
                LastModifiedBy = m.LastModifiedBy,
                CreatedByName = _unitOfWork.UserRepo.GetById(m.CreatedBy)?.Email ?? "Không xác định",
                LastModifiedByName = _unitOfWork.UserRepo.GetById(m.LastModifiedBy)?.Email ?? "Không xác định",
                DiscountCodeType = m.DiscountCodeType,
                
            });

            return new DataTableJson(data, query.draw, totalRecords);
        }

        public async Task<DiscountCodeDto?> GetByIdAsync(Guid id)
        {
            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
            if (entity == null) return null;

            return new DiscountCodeDto
            {
                Id = entity.Id,
                Code = entity.Code,
                Description = entity.Description,
                DiscountValue = entity.DiscountValue,
                DiscountValueMax = entity.DiscountValueMax,

                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                UsageLimit = entity.UsageLimit,
                UsedCount = entity.UsedCount,
                MinOrderAmount = entity.MinOrderAmount,
                ModerationStatus = entity.ModerationStatus,
                Created = entity.Created,
                LastModified = entity.LastModified,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedByName = (await _unitOfWork.UserRepo.GetByIdAsync(entity.CreatedBy))?.Email ?? "Không xác định",
                LastModifiedByName = (await _unitOfWork.UserRepo.GetByIdAsync(entity.LastModifiedBy))?.Email ?? "Không xác định",
                DiscountCodeType = entity.DiscountCodeType
            };
        }

        public async Task<Guid> CreateAsync(DiscountCodeDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                if (string.IsNullOrWhiteSpace(item.Code))
                    throw new Exception("Mã giảm giá không được để trống");

                var discountexsit = _unitOfWork.DiscountCodeRepo.FindWhere(x => x.Code.ToLower() == item.Code.ToLower()&&x.EndDate>DateTime.Now);
                if (discountexsit.Any())
                {
                    throw new Exception("Mã giảm giá đã tồn tại");

                }

                var entity = new DiscountCode
                {
                    Id = Guid.NewGuid(),
                    Code = item.Code.ToUpper().Trim(),
                    Description = item.Description,
                    DiscountValue = item.DiscountValue,
                    DiscountValueMax = item.DiscountValueMax,

                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    UsageLimit = item.UsageLimit,
                    UsedCount = item.UsedCount,
                    MinOrderAmount = item.MinOrderAmount,
                    ModerationStatus = ModerationStatus.Rejected,
                    DiscountCodeType = item.DiscountCodeType
                };

                _unitOfWork.DiscountCodeRepo.Add(entity);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi tạo mã giảm giá: " + ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, DiscountCodeDto item)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
                if (entity == null)
                    throw new Exception("Không tìm thấy mã giảm giá");

                var discountexsit = _unitOfWork.DiscountCodeRepo.FindWhere(x => x.Code.ToLower() == item.Code.ToLower() && x.EndDate > DateTime.Now&&x.Id!=id);
                if (discountexsit.Any())
                {
                    throw new Exception("Mã giảm giá đã tồn tại");

                }

                entity.Code = item.Code.ToUpper().Trim();
                entity.Description = item.Description;
                entity.DiscountValue = item.DiscountValue;
                entity.DiscountValueMax = item.DiscountValueMax;

                entity.StartDate = item.StartDate;
                entity.EndDate = item.EndDate;
                entity.UsageLimit = item.UsageLimit;
                entity.UsedCount = item.UsedCount;
                entity.MinOrderAmount = item.MinOrderAmount;
                entity.DiscountCodeType = item.DiscountCodeType;

                _unitOfWork.DiscountCodeRepo.Update(entity);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi cập nhật mã giảm giá: " + ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _unitOfWork.DiscountCodeRepo.Delete(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> ApproveAsync(Guid id)
        {
            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.ModerationStatus = ModerationStatus.Approved;
            _unitOfWork.DiscountCodeRepo.Update(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> RejectAsync(Guid id)
        {
            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.ModerationStatus = ModerationStatus.Rejected;
            _unitOfWork.DiscountCodeRepo.Update(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        

       

        public async Task<DiscountCodeDto> GetDiscountHoatDongById(Guid id)
        {
            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
            if (entity == null) 
                throw new ArgumentNullException("Không tìm thấy mã giảm giá");
            if (entity.ModerationStatus== ModerationStatus.Approved &&entity.StartDate<=DateTime.Now && entity.EndDate>=DateTime.Now && entity.UsedCount <entity.UsageLimit)
            {
                return new DiscountCodeDto
                {
                    Id = entity.Id,
                    Code = entity.Code,
                    Description = entity.Description,
                    DiscountValue = entity.DiscountValue,
                    DiscountValueMax = entity.DiscountValueMax,

                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    UsageLimit = entity.UsageLimit,
                    UsedCount = entity.UsedCount,
                    MinOrderAmount = entity.MinOrderAmount,
                    ModerationStatus = entity.ModerationStatus,

                    DiscountCodeType = entity.DiscountCodeType
                };

            }
            else
            {
                throw new ArgumentNullException("Mã giảm giá hết hạn");
            }

            
        }
    }
}
