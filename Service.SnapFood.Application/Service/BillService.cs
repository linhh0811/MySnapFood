using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;


using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Service.SnapFood.Domain.Query;

using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Service.SnapFood.Application.Service
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get dữ liệu
        public async Task<BillDetailsDto?> GetDetailByIdAsync(Guid id)
        {
            var bill = await _unitOfWork.BillRepo.GetByIdAsync(id);
            if (bill == null)
                return null;

            var allDetails = await _unitOfWork.BillDetailsRepo.GetAllAsync();
            var billDetails = allDetails.Where(d => d.BillId == id).ToList();

            return new BillDetailsDto
            {
                Id = bill.Id,
                ItemsName = string.Empty, 
                ImageUrl = string.Empty,
                Quantity = 0,
                Price = 0,
                PriceEndow = 0
            };
        }

       
        public async Task<List<Bill>> GetAllAsync()
        {
            var bills = await _unitOfWork.BillRepo.GetAllAsync();
            return bills.ToList(); // Chuyển đổi từ IEnumerable sang List
        }

        public async Task<Bill> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("ID hóa đơn không hợp lệ", nameof(id));

            var bill = await _unitOfWork.BillRepo.GetByIdAsync(id);
            if (bill == null)
                throw new Exception("Hóa đơn không tồn tại");

            return bill;
        }
        #endregion

        #region Thêm, sửa
        public async Task<Guid> CreateAsync(BillDto item)
        {
            try
            {
                // Validation
                await ValidateBillDto(item, isCreate: true);

                var bill = new Bill
                {
                    BillCode = item.BillCode,
                    UserId = item.UserId,
                    StoreId = item.StoreId,
                    Status = item.Status,
                    TotalAmount = item.TotalAmount,
                    TotalAmountEndow = item.TotalAmountEndow
                    // Không gán trực tiếp Created, giả định được xử lý trong FillDataForInsert
                };

                // Giả định IntermediaryEntity có phương thức FillDataForInsert
                bill.FillDataForInsert(Guid.NewGuid()); // Thay Guid.NewGuid() bằng userId thực tế nếu có

                _unitOfWork.BillRepo.Add(bill);
                await _unitOfWork.CompleteAsync();
                return bill.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, BillDto item)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("ID hóa đơn không hợp lệ", nameof(id));

                // Validation
                await ValidateBillDto(item, isCreate: false);

                var bill = await _unitOfWork.BillRepo.GetByIdAsync(id);
                if (bill == null)
                    throw new Exception("Không tìm thấy hóa đơn");

                bill.BillCode = item.BillCode;
                bill.UserId = item.UserId;
                bill.StoreId = item.StoreId;
                bill.Status = item.Status;
                bill.TotalAmount = item.TotalAmount;
                bill.TotalAmountEndow = item.TotalAmountEndow;

                _unitOfWork.BillRepo.Update(bill);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Cập nhật trạng thái
        public async Task<bool> UpdateStatusAsync(Guid id, StatusOrder status)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("ID hóa đơn không hợp lệ", nameof(id));

                var bill = await _unitOfWork.BillRepo.GetByIdAsync(id);
                if (bill == null)
                    throw new Exception("Không tìm thấy hóa đơn");

                bill.Status = status;

                _unitOfWork.BillRepo.Update(bill);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Validate
        private async Task ValidateBillDto(BillDto item, bool isCreate)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Thông tin hóa đơn không được để trống");

            if (string.IsNullOrWhiteSpace(item.BillCode))
                throw new ArgumentException("Mã hóa đơn không được để trống", nameof(item.BillCode));

            if (item.UserId == Guid.Empty)
                throw new ArgumentException("ID người dùng không hợp lệ", nameof(item.UserId));

            if (item.StoreId == Guid.Empty)
                throw new ArgumentException("ID cửa hàng không hợp lệ", nameof(item.StoreId));

            if (item.TotalAmount < 0)
                throw new ArgumentException("Tổng tiền không được âm", nameof(item.TotalAmount));

            if (item.TotalAmountEndow < 0)
                throw new ArgumentException("Tổng tiền ưu đãi không được âm", nameof(item.TotalAmountEndow));

            if (isCreate && (await _unitOfWork.BillRepo.GetAllAsync()).Any(b => b.BillCode == item.BillCode))
                throw new ArgumentException("Mã hóa đơn đã tồn tại", nameof(item.BillCode));
        }


        #endregion
        //public async Task<List<BillDetailsDto>> GetBillDetailsByBillIdAsync(Guid billId)
        //{
        //    var allDetails = await _unitOfWork.BillDetailsRepo.GetAllAsync(); // Trả về IEnumerable<BillDetails>

        //    var result = allDetails
        //        .Where(b => b.BillId == billId) // Lọc theo BillId
        //        .Select(b => new BillDetailsDto
        //        {
        //            ItemsName = b.ItemsName,
        //            ImageUrl = b.ImageUrl,
        //            Quantity = b.Quantity,
        //            Price = b.Price,
        //            PriceEndow = b.PriceEndow
        //        })
        //        .ToList(); // Chuyển sang List

        //    return result;

        //}

       

        public async Task<BillViewDto> GetDetailsByBillIdAsync(Guid billId)
        {
          
            var bill =await _unitOfWork.BillRepo.GetByIdAsync(billId);
            if (bill is not null)
            {
                BillViewDto billViewDto = new BillViewDto();

                billViewDto.Id = billId;
                billViewDto.BillCode = bill.BillCode;
                billViewDto.TotalAmount = bill.TotalAmount;
                billViewDto.TotalAmountEndow = bill.TotalAmountEndow;
                billViewDto.Status = bill.Status;
                billViewDto.Created = bill.Created;
                billViewDto.BillDetailsDtos = _unitOfWork.BillDetailsRepo.FindWhere(x=>x.BillId== billId)
                    .Select(x=> new BillDetailsDto()
                    {
                        Id = x.Id,
                        BillId = x.BillId,
                        ItemsName = x.ItemsName,
                        ImageUrl = x.ImageUrl,
                        Quantity= x.Quantity,
                        Price= x.Price,
                        PriceEndow= x.PriceEndow,
                        ItemType= x.ItemType,

                    }).ToList();
                foreach (var item in billViewDto.BillDetailsDtos)
                {
                    if (item.ItemType== ItemType.Combo)
                    {
                        item.Product = _unitOfWork.ComboItemsArchiveRepo.FindWhere(x => x.BillDetailsId == item.Id)
                            .Select(x => new ComboItemsArchiveDto()
                        {
                            Id = x.Id,
                            BillDetailsId=x.BillDetailsId,
                            ProductName=x.ProductName,
                            Quantity=x.Quantity,
                            ImageUrl=x.ImageUrl,
                            Price=x.Price,
                           
                        }).ToList();
                    }
                }
                var billPayment = _unitOfWork.BillPaymentRepo.FirstOrDefault(x => x.BillId == billId);
                if (billPayment is not null)
                {
                    billViewDto.BillPaymentDto = new BillPaymentDto()
                    {
                        Id = billPayment.Id,
                        BillId = billPayment.BillId,
                        PaymentType = billPayment.PaymentType,
                        Amount = billPayment.Amount,
                        PaymentDate = billPayment.PaymentDate,
                        PaymentStatus = billPayment.PaymentStatus,
                    };
                }
                billViewDto.BillNotesDtos = _unitOfWork.BillNotesRepo.FindWhere(x => x.BillId == billId)
                    .Select(x => new BillNotesDto()
                    {
                        Id = x.Id,
                        BillId = x.BillId,
                        NoteContent = x.NoteContent,
                        NoteType = x.NoteType,
                        Created=x.Created,
                        CreatedBy=x.CreatedBy,
                    }).ToList();

                var billDelivery = _unitOfWork.BillDeliveryRepo.FirstOrDefault(x => x.BillId == billId);
                if (billDelivery is not null)
                {
                    billViewDto.BillDeliveryDto = new BillDeliveryDto()
                    {
                        Id = billDelivery.Id,
                        BillId = billDelivery.BillId,
                        ReceivingType=billDelivery.ReceivingType,
                        ReceiverName = billDelivery.ReceiverName,
                        ReceiverPhone= billDelivery.ReceiverPhone,
                        ReceiverAddress= billDelivery.ReceiverAddress,
                        DeliveryFee= billDelivery.DeliveryFee,
                        Distance= billDelivery.Distance,
                    };
                }

                return billViewDto;
            }
            return new BillViewDto();
            
        }

        public DataTableJson GetPage(BillQuery query)
        {
         
            try
            {
                if (query == null || query.gridRequest == null)
                    throw new ArgumentNullException();

                int totalRecords = 0;

                var dataQuery = _unitOfWork.BillRepo.FilterData(
                    q => query.Status.HasValue ?
                        q.Where(x => (int)x.Status == (int)query.Status.Value) : q,
                    query.gridRequest,
                    ref totalRecords
                );


                var allUsers = _unitOfWork.UserRepo.GetAll().ToList();

                var data = dataQuery.ToList().Select(m => new BillDto
                {
                    Id = m.Id,
                    BillCode = m.BillCode,
                    UserId = m.UserId,
                    FullName = allUsers.FirstOrDefault(u => u.Id == m.UserId)?.FullName ?? string.Empty,
                    StoreId = m.StoreId,
                    Status = m.Status,
                 
                    TotalAmount = m.TotalAmount,
                    TotalAmountEndow = m.TotalAmountEndow,
                    Created = m.Created
                }).ToList();

                return new DataTableJson(data, query.draw, totalRecords);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi phân trang hóa đơn: " + ex.Message);
            }
        }
        
         public async Task<bool> ApplyDiscountAsync(Guid billId, Guid discountCodeId, Guid userId)
        {
            try
            {
                var bill = await _unitOfWork.BillRepo.GetByIdAsync(billId);
                if (bill == null)
                    throw new Exception("Không tìm thấy hóa đơn");

                var discountCode = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(discountCodeId);
                if (discountCode == null || !discountCode.IsActive || discountCode.EndDate < DateTime.Now)
                    throw new Exception("Mã giảm giá không hợp lệ hoặc đã hết hạn");

                if (bill.UserId != userId)
                    throw new Exception("Người dùng không hợp lệ cho hóa đơn này");

                if (discountCode.UsageLimit > 0 && discountCode.UsedCount >= discountCode.UsageLimit)
                    throw new Exception("Mã giảm giá đã hết lượt sử dụng");

                if (bill.TotalAmount < discountCode.MinOrderAmount)
                    throw new Exception($"Hóa đơn cần tối thiểu {discountCode.MinOrderAmount:N0}đ để áp dụng mã giảm");

                decimal discountAmount = 0;

                switch (discountCode.DiscountCodeType)
                {
                    case DiscountCodeType.Money:
                        discountAmount = discountCode.DiscountAmount;
                        break;

                    case DiscountCodeType.Percent:
                        discountAmount = bill.TotalAmount * (discountCode.DiscountAmount / 100);
                        break;

                    default:
                        throw new Exception("Loại mã giảm giá không hợp lệ");
                }

                discountAmount = Math.Min(discountAmount, bill.TotalAmount);


                bill.TotalAmountEndow = discountAmount;
                bill.TotalAmount -= discountAmount;

                discountCode.UsedCount++;

                _unitOfWork.BillRepo.Update(bill);
                _unitOfWork.DiscountCodeRepo.Update(discountCode);

                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi áp dụng mã giảm giá: {ex.Message}");
            }
        }

       
    }
}