//using Service.SnapFood.Application.Dtos;
//using Service.SnapFood.Application.Interfaces;
//using Service.SnapFood.Domain.Entitys;
//using Service.SnapFood.Domain.Enums;
//using Service.SnapFood.Domain.Interfaces.UnitOfWork;
//using Service.SnapFood.Domain.Query;
//using Service.SnapFood.Share.Model.Commons;
//using Service.SnapFood.Share.Model.SQL;

//namespace Service.SnapFood.Application.Service
//{
//    public class DiscountCodeService : IDiscountCodeService
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public DiscountCodeService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public DataTableJson GetPaged(VoucherQuery query)
//        {
//            int totalRecords = 0;

//            var dataQuery = _unitOfWork.DiscountCodeRepo.FilterData(
//                q => q.Where(x => query.ModerationStatus == ModerationStatus.None ? true : x.ModerationStatus == query.ModerationStatus),
//                query.gridRequest,
//                ref totalRecords
//            );

//            var data = dataQuery.ToList().Select((m, i) => new DiscountCodeDto
//            {
//                Id = m.Id,
//                Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
//                Code = m.Code,
//                Description = m.Description,
//                DiscountAmount = m.DiscountAmount,
//                StartDate = m.StartDate,
//                EndDate = m.EndDate,
//                UsageLimit = m.UsageLimit,
//                UsedCount = m.UsedCount,
//                MinOrderAmount = m.MinOrderAmount,
//                IsActive = m.IsActive,
//                ApplyToOrderTypes = m.ApplyToOrderTypes,
//                ModerationStatus = m.ModerationStatus,
//                Created = m.Created,
//                LastModified = m.LastModified,
//                CreatedBy = m.CreatedBy,
//                LastModifiedBy = m.LastModifiedBy,
//                CreatedByName = _unitOfWork.UserRepo.GetById(m.CreatedBy)?.Email ?? "Không xác định",
//                LastModifiedByName = _unitOfWork.UserRepo.GetById(m.LastModifiedBy)?.Email ?? "Không xác định"
//            });

//            return new DataTableJson(data, query.draw, totalRecords);
//        }

//        public async Task<DiscountCodeDto?> GetByIdAsync(Guid id)
//        {
//            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
//            if (entity == null) return null;

//            return new DiscountCodeDto
//            {
//                Id = entity.Id,
//                Code = entity.Code,
//                Description = entity.Description,
//                DiscountAmount = entity.DiscountAmount,
//                StartDate = entity.StartDate,
//                EndDate = entity.EndDate,
//                UsageLimit = entity.UsageLimit,
//                UsedCount = entity.UsedCount,
//                MinOrderAmount = entity.MinOrderAmount,
//                IsActive = entity.IsActive,
//                ApplyToOrderTypes = entity.ApplyToOrderTypes,
//                ModerationStatus = entity.ModerationStatus,
//                Created = entity.Created,
//                LastModified = entity.LastModified,
//                CreatedBy = entity.CreatedBy,
//                LastModifiedBy = entity.LastModifiedBy,
//                CreatedByName = (await _unitOfWork.UserRepo.GetByIdAsync(entity.CreatedBy))?.Email ?? "Không xác định",
//                LastModifiedByName = (await _unitOfWork.UserRepo.GetByIdAsync(entity.LastModifiedBy))?.Email ?? "Không xác định"
//            };
//        }

//        public async Task<Guid> CreateAsync(DiscountCodeDto item)
//        {
//            _unitOfWork.BeginTransaction();
//            try
//            {
//                if (string.IsNullOrWhiteSpace(item.Code))
//                    throw new Exception("Mã giảm giá không được để trống");

//                var entity = new DiscountCode
//                {
//                    Id = Guid.NewGuid(),
//                    Code = item.Code,
//                    Description = item.Description,
//                    DiscountAmount = item.DiscountAmount,
//                    StartDate = item.StartDate,
//                    EndDate = item.EndDate,
//                    UsageLimit = item.UsageLimit,
//                    UsedCount = item.UsedCount,
//                    MinOrderAmount = item.MinOrderAmount,
//                    IsActive = item.IsActive,
//                    ApplyToOrderTypes = item.ApplyToOrderTypes,
//                    ModerationStatus = ModerationStatus.Rejected
//                };

//                _unitOfWork.DiscountCodeRepo.Add(entity);
//                await _unitOfWork.CompleteAsync();
//                await _unitOfWork.CommitAsync();
//                return entity.Id;
//            }
//            catch (Exception ex)
//            {
//                await _unitOfWork.RollbackAsync();
//                throw new Exception("Lỗi khi tạo mã giảm giá: " + ex.Message);
//            }
//        }

//        public async Task<bool> UpdateAsync(Guid id, DiscountCodeDto item)
//        {
//            _unitOfWork.BeginTransaction();
//            try
//            {
//                var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
//                if (entity == null)
//                    throw new Exception("Không tìm thấy mã giảm giá");

//                entity.Code = item.Code;
//                entity.Description = item.Description;
//                entity.DiscountAmount = item.DiscountAmount;
//                entity.StartDate = item.StartDate;
//                entity.EndDate = item.EndDate;
//                entity.UsageLimit = item.UsageLimit;
//                entity.UsedCount = item.UsedCount;
//                entity.MinOrderAmount = item.MinOrderAmount;
//                entity.IsActive = item.IsActive;
//                entity.ApplyToOrderTypes = item.ApplyToOrderTypes;

//                _unitOfWork.DiscountCodeRepo.Update(entity);
//                await _unitOfWork.CompleteAsync();
//                await _unitOfWork.CommitAsync();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                await _unitOfWork.RollbackAsync();
//                throw new Exception("Lỗi khi cập nhật mã giảm giá: " + ex.Message);
//            }
//        }

//        public async Task<bool> DeleteAsync(Guid id)
//        {
//            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
//            if (entity == null) return false;

//            _unitOfWork.DiscountCodeRepo.Delete(entity);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<bool> ApproveAsync(Guid id)
//        {
//            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
//            if (entity == null) return false;

//            entity.ModerationStatus = ModerationStatus.Approved;
//            _unitOfWork.DiscountCodeRepo.Update(entity);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }

//        public async Task<bool> RejectAsync(Guid id)
//        {
//            var entity = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(id);
//            if (entity == null) return false;

//            entity.ModerationStatus = ModerationStatus.Rejected;
//            _unitOfWork.DiscountCodeRepo.Update(entity);
//            await _unitOfWork.CompleteAsync();
//            return true;
//        }




//        public async Task<decimal> ApplyDiscountCodeToBillAsync(Guid discountCodeId, Guid billId, Guid userId)
//        {
//            _unitOfWork.BeginTransaction();
//            try
//            {
//                var discount = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(discountCodeId);
//                if (discount == null)
//                    throw new Exception("Mã giảm giá không tồn tại");

//                if (!discount.IsActive || discount.ModerationStatus != ModerationStatus.Approved)
//                    throw new Exception("Mã giảm giá không hợp lệ hoặc chưa được duyệt");

//                var now = DateTime.Now;
//                if (now < discount.StartDate || now > discount.EndDate)
//                    throw new Exception("Mã giảm giá đã hết hạn hoặc chưa được kích hoạt");

//                if (discount.UsageLimit > 0 && discount.UsedCount >= discount.UsageLimit)
//                    throw new Exception("Mã giảm giá đã hết lượt sử dụng");

//                var bill = await _unitOfWork.BillRepo.GetByIdAsync(billId);
//                if (bill == null)
//                    throw new Exception("Hóa đơn không tồn tại");

//                if (bill.TotalAmount < discount.MinOrderAmount)
//                    throw new Exception($"Đơn hàng không đủ điều kiện áp dụng mã giảm giá (yêu cầu tối thiểu {discount.MinOrderAmount:N0}đ)");

//                // Trừ tiền vào đơn hàng
//                decimal discountAmount = discount.DiscountAmount;
//                decimal finalAmount = bill.TotalAmount - discountAmount;
//                if (finalAmount < 0) finalAmount = 0;

//                // Cập nhật hóa đơn
//                bill.DiscountCodeId = discount.Id;
//                bill.DiscountAmount = discountAmount;
//                bill.TotalAfterDiscount = finalAmount;

//                _unitOfWork.BillRepo.Update(bill);

//                // Cập nhật UsedCount
//                discount.UsedCount += 1;
//                _unitOfWork.DiscountCodeRepo.Update(discount);

//                // Tạo DiscountCodeUsage log
//                var usage = new DiscountCodeUsage
//                {
//                    Id = Guid.NewGuid(),
//                    DiscountCodeId = discount.Id,
//                    UserId = userId,
//                    BillId = bill.Id,
//                    UsedAt = now
//                };
//                _unitOfWork.DiscountCodeUsageRepo.Add(usage);

//                await _unitOfWork.CompleteAsync();
//                await _unitOfWork.CommitAsync();

//                return finalAmount;
//            }
//            catch (Exception ex)
//            {
//                await _unitOfWork.RollbackAsync();
//                throw new Exception("Lỗi khi áp dụng mã giảm giá: " + ex.Message);
//            }
//        }


//    }


//}


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
                q => q.Where(x => query.ModerationStatus == ModerationStatus.None ? true : x.ModerationStatus == query.ModerationStatus),
                query.gridRequest,
                ref totalRecords
            );

            var data = dataQuery.ToList().Select((m, i) => new DiscountCodeDto
            {
                Id = m.Id,
                Index = ((query.gridRequest.page - 1) * query.gridRequest.pageSize) + i + 1,
                Code = m.Code,
                Description = m.Description,
                DiscountAmount = m.DiscountAmount,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                UsageLimit = m.UsageLimit,
                UsedCount = m.UsedCount,
                MinOrderAmount = m.MinOrderAmount,
                IsActive = m.IsActive,
                ApplyToOrderTypes = m.ApplyToOrderTypes,
                ModerationStatus = m.ModerationStatus,
                Created = m.Created,
                LastModified = m.LastModified,
                CreatedBy = m.CreatedBy,
                LastModifiedBy = m.LastModifiedBy,
                CreatedByName = _unitOfWork.UserRepo.GetById(m.CreatedBy)?.Email ?? "Không xác định",
                LastModifiedByName = _unitOfWork.UserRepo.GetById(m.LastModifiedBy)?.Email ?? "Không xác định",
                DiscountCodeType = m.DiscountCodeType
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
                DiscountAmount = entity.DiscountAmount,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                UsageLimit = entity.UsageLimit,
                UsedCount = entity.UsedCount,
                MinOrderAmount = entity.MinOrderAmount,
                IsActive = entity.IsActive,
                ApplyToOrderTypes = entity.ApplyToOrderTypes,
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

                var entity = new DiscountCode
                {
                    Id = Guid.NewGuid(),
                    Code = item.Code,
                    Description = item.Description,
                    DiscountAmount = item.DiscountAmount,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    UsageLimit = item.UsageLimit,
                    UsedCount = item.UsedCount,
                    MinOrderAmount = item.MinOrderAmount,
                    IsActive = item.IsActive,
                    ApplyToOrderTypes = item.ApplyToOrderTypes,
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

                entity.Code = item.Code;
                entity.Description = item.Description;
                entity.DiscountAmount = item.DiscountAmount;
                entity.StartDate = item.StartDate;
                entity.EndDate = item.EndDate;
                entity.UsageLimit = item.UsageLimit;
                entity.UsedCount = item.UsedCount;
                entity.MinOrderAmount = item.MinOrderAmount;
                entity.IsActive = item.IsActive;
                entity.ApplyToOrderTypes = item.ApplyToOrderTypes;
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

        public async Task<decimal> ApplyDiscountCodeToBillAsync(Guid discountCodeId, Guid billId, Guid userId)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var discount = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(discountCodeId);
                if (discount == null)
                    throw new Exception("Mã giảm giá không tồn tại");

                if (!discount.IsActive || discount.ModerationStatus != ModerationStatus.Approved)
                    throw new Exception("Mã giảm giá không hợp lệ hoặc chưa được duyệt");

                var now = DateTime.Now;
                if (now < discount.StartDate || now > discount.EndDate)
                    throw new Exception("Mã giảm giá đã hết hạn hoặc chưa được kích hoạt");

                if (discount.UsageLimit > 0 && discount.UsedCount >= discount.UsageLimit)
                    throw new Exception("Mã giảm giá đã hết lượt sử dụng");

                var bill = await _unitOfWork.BillRepo.GetByIdAsync(billId);
                if (bill == null)
                    throw new Exception("Hóa đơn không tồn tại");

                if (bill.TotalAmount < discount.MinOrderAmount)
                    throw new Exception($"Đơn hàng không đủ điều kiện áp dụng mã giảm giá (yêu cầu tối thiểu {discount.MinOrderAmount:N0}đ)");

                decimal discountAmount = 0;

                // ✅ Tính theo loại mã giảm giá
                switch (discount.DiscountCodeType)
                {
                    case DiscountCodeType.Money:
                        discountAmount = discount.DiscountAmount;
                        break;

                    case DiscountCodeType.Percent:
                        discountAmount = bill.TotalAmount * (discount.DiscountAmount / 100m);
                        break;

                    default:
                        throw new Exception("Loại mã giảm giá không hợp lệ");
                }

                // Không để số âm
                decimal finalAmount = bill.TotalAmount - discountAmount;
                if (finalAmount < 0) finalAmount = 0;

                // Cập nhật hóa đơn
                bill.DiscountCodeId = discount.Id;
                bill.DiscountAmount = discountAmount;
                bill.TotalAfterDiscount = finalAmount;

                _unitOfWork.BillRepo.Update(bill);

                // Cập nhật UsedCount
                discount.UsedCount += 1;
                _unitOfWork.DiscountCodeRepo.Update(discount);

                // Tạo log sử dụng
                var usage = new DiscountCodeUsage
                {
                    Id = Guid.NewGuid(),
                    DiscountCodeId = discount.Id,
                    UserId = userId,
                    BillId = bill.Id,
                    UsedAt = now
                };
                _unitOfWork.DiscountCodeUsageRepo.Add(usage);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitAsync();

                return finalAmount;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Lỗi khi áp dụng mã giảm giá: " + ex.Message);
            }
        }
    }
}
