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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public List<BillDto> GetByUser(Guid id)
        {
            var bills = _unitOfWork.BillRepo.FindWhere(x => x.UserId == id).Select(b => new BillDto
            {
                Id = b.Id,
                BillCode = b.BillCode,
                UserId = b.UserId,
                StoreId = b.StoreId,
                Status = b.Status,
                TotalAmount = b.TotalAmount,
                TotalAmountEndow = b.TotalAmountEndow,
                DiscountAmount = b.DiscountAmount,
                PhiVanChuyen = _unitOfWork.BillDeliveryRepo.FirstOrDefault(x => x.BillId == b.Id)?.DeliveryFee??0,
                Created = b.Created
            }).OrderByDescending(x => x.Created).ToList(); ;
            return bills;
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
        public async Task<bool> UpdateStatusAsync(Guid id, UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("ID hóa đơn không hợp lệ", nameof(id));

                var bill = await _unitOfWork.BillRepo.GetByIdAsync(id);
                if (bill == null)
                    throw new Exception("Không tìm thấy hóa đơn");

                bill.Status = updateOrderStatusDto.StatusOrder;

                _unitOfWork.BillRepo.Update(bill);
                await _unitOfWork.CompleteAsync();

                string NoteContent = string.Empty;
                if (updateOrderStatusDto.StatusOrder== StatusOrder.Pending)
                {
                    NoteContent = "Đơn hàng chờ xác nhận";
                }else if (updateOrderStatusDto.StatusOrder == StatusOrder.Confirmed)
                {
                    NoteContent = "Đơn hàng đã được xác nhận";
                }
                else if (updateOrderStatusDto.StatusOrder == StatusOrder.DangChuanBi)
                {
                    NoteContent = "Đơn hàng đã được xác nhận, đang chuẩn bị hàng";
                }
                else if (updateOrderStatusDto.StatusOrder == StatusOrder.Shipping)
                {
                    NoteContent = "Đơn hàng đã được chuẩn bị xong và chuẩn bị giao cho khách hàng";
                }
                else if (updateOrderStatusDto.StatusOrder == StatusOrder.Completed)
                {
                    NoteContent = "Đơn hàng đã được hoàn thành";
                }
                else if (updateOrderStatusDto.StatusOrder == StatusOrder.Cancelled)
                {
                    NoteContent = "Đơn hàng đã bị hủy. Lý do: "+ updateOrderStatusDto.Reason;
                }

                if (!string.IsNullOrEmpty(NoteContent))
                {
                    BillNotes billNotes = new BillNotes()
                    {
                        BillId = bill.Id,
                        NoteType = NoteType.Internal,
                        NoteContent=NoteContent,
                        CreatedBy= Guid.Empty

                    };
                    _unitOfWork.BillNotesRepo.Update(billNotes);
                    await _unitOfWork.CompleteAsync();
                }

                if (updateOrderStatusDto.StatusOrder == StatusOrder.Completed)
                {
                    var billDetails = _unitOfWork.BillDetailsRepo.FindWhere(x => x.BillId == bill.Id);
                    foreach (var item in billDetails)
                    {
                        if (item.ItemType== ItemType.Product)
                        {
                            var product = _unitOfWork.ProductRepo.GetById(item.ItemId);
                            if (product is not null)
                            {
                                product.Quantity += item.Quantity;
                                _unitOfWork.ProductRepo.Update(product);
                                await _unitOfWork.CompleteAsync();
                            }
                        }
                        else
                        {
                            var combo = _unitOfWork.ComboRepo.GetById(item.ItemId);
                            if (combo is not null)
                            {
                                combo.Quantity += item.Quantity;
                                _unitOfWork.ComboRepo.Update(combo);
                                await _unitOfWork.CompleteAsync();
                                var products = _unitOfWork.ComboItemsArchiveRepo.FindWhere(x => x.BillDetailsId == item.Id);
                                foreach (var p in products)
                                {
                                    var product = _unitOfWork.ProductRepo.GetById(p.ProductId);
                                    if (product is not null)
                                    {
                                        product.Quantity += item.Quantity;
                                        _unitOfWork.ProductRepo.Update(product);
                                        await _unitOfWork.CompleteAsync();
                                    }
                                }
                            }
                        }
                    }
                }
                
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



       
        public async Task<BillViewDto> GetBillDetailsByBillIdAsync(Guid billId)
        {
          
            var bill =await _unitOfWork.BillRepo.GetByIdAsync(billId);
            if (bill is not null)
            {
                BillViewDto billViewDto = new BillViewDto();

                billViewDto.Id = billId;
                billViewDto.BillCode = bill.BillCode;
                billViewDto.TotalAmount = bill.TotalAmount;
                billViewDto.TotalAmountEndow = bill.TotalAmountEndow;
                billViewDto.DiscountAmount = bill.DiscountAmount;

                billViewDto.PhuongThucDatHang = bill.PhuongThucDatHang;
                billViewDto.ReceivingType = bill.ReceivingType;


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
                     q => q.Where(x =>
                         (query.Status == StatusOrder.None || x.Status == query.Status) &&
                         (!query.IsBanHang || (x.Status != StatusOrder.Completed && x.Status != StatusOrder.Cancelled))
                     ),
                     query.gridRequest,
                     ref totalRecords
                 );


                var allUsers = _unitOfWork.UserRepo.GetAll().ToList();

                var data = dataQuery.ToList().Select(m => new BillDto
                {
                    Id = m.Id,
                    BillCode = m.BillCode,
                    UserId = m.UserId,
                    FullName = allUsers.FirstOrDefault(u => u.Id == m.UserId)?.Email ?? string.Empty,
                    StoreId = m.StoreId,
                    Status = m.Status,                
                    TotalAmount = m.TotalAmount,
                    TotalAmountEndow = m.TotalAmountEndow,
                    DiscountAmount = m.DiscountAmount,
                    PhiVanChuyen = _unitOfWork.BillDeliveryRepo.FirstOrDefault(x => x.BillId == m.Id)?.DeliveryFee ?? 0,
                    Created = m.Created,
                    ReceivingType=m.ReceivingType,
                    PhuongThucDatHang = m.PhuongThucDatHang
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
                if (discountCode == null  || discountCode.EndDate < DateTime.Now)
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
                        discountAmount = discountCode.DiscountValue;
                        break;

                    case DiscountCodeType.Percent:
                        discountAmount = bill.TotalAmount * (discountCode.DiscountValue / 100);
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

        

        

        public async Task<decimal> GetTotalRevenueAsync()
        {
            var bills = await _unitOfWork.BillRepo.FindAllAsync(x => true);


            decimal totalRevenue = bills.Sum(x => x.TotalAmount - x.TotalAmountEndow);

            return totalRevenue;
        }

        public async Task<int> GetTotalInvoicesAsync()
        {
            var bills = await _unitOfWork.BillRepo.FindAllAsync(x => true);
            return bills.Count();
        }

        public async Task<List<DailyRevenueDto>> GetDailyRevenueAsync(DateTime fromDate, DateTime toDate)
        {
            var bills = await _unitOfWork.BillRepo.FindAllAsync(x =>
        x.Created >= fromDate.Date && x.Created <= toDate.Date.AddDays(1).AddTicks(-1));

            var result = bills
                .GroupBy(b => b.Created.Date)
                .Select(g => new DailyRevenueDto
                {
                    Date = g.Key,
                    Amount = g.Sum(b => b.TotalAmount - b.TotalAmountEndow),
                 
                })
                .OrderBy(x => x.Date)
                .ToList();

            return result;
        }

       
    
       

        public async Task<List<ChartItemDto>> GetWeeklyRevenueAsync(DateTime? from = null, DateTime? to = null)
        {
            var bills = await _unitOfWork.BillRepo.FindAllAsync(b =>
         b.Status == StatusOrder.Completed &&
         (!from.HasValue || b.Created.Date >= from.Value.Date) &&
         (!to.HasValue || b.Created.Date <= to.Value.Date));

            return bills
                .GroupBy(b => new
                {
                    Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(b.Created, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday),
                    Month = b.Created.Month,
                    Year = b.Created.Year
                })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Week)
                .Select(g => new ChartItemDto
                {
                    Label = $"Tuần {g.Key.Week} ({g.Key.Month}/{g.Key.Year})",
                    Value = g.Sum(x => x.TotalAmount - x.TotalAmountEndow),
                   
                })
                .ToList();
        }

        public async Task<List<ChartItemDto>> GetMonthlyRevenueAsync(DateTime? from = null, DateTime? to = null)
        {
            var bills = await _unitOfWork.BillRepo.FindAllAsync(b =>
             b.Status == StatusOrder.Completed &&
             (!from.HasValue || b.Created.Date >= from.Value.Date) &&
             (!to.HasValue || b.Created.Date <= to.Value.Date));

            return bills
                .GroupBy(b => new { b.Created.Year, b.Created.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select(g => new ChartItemDto
                {
                    Label = $"{g.Key.Month}/{g.Key.Year}",
                    Value = g.Sum(x => x.TotalAmount - x.TotalAmountEndow)
                })
                .ToList();
        }

       

      
       

        public async Task<List<ChartItemDto>> GetTopBestSellingCombosAsync(int top = 5)
        {
            var query = _unitOfWork.BillDetailsRepo.Query()
       .Where(bd => bd.ItemType == ItemType.Combo); // ✅ chỉ lọc combo

            return await query
                .GroupBy(bd => new { bd.ItemsName, bd.ImageUrl })
                .Select(g => new ChartItemDto
                {
                    Label = g.Key.ItemsName,
                    Value = g.Sum(bd => bd.Quantity),
                    ImageUrl = g.Key.ImageUrl ?? "/images/default.png"
                })
                .OrderByDescending(x => x.Value)
                .Take(top)
                .ToListAsync();
        }

        public async Task<List<ChartItemDto>> GetTopBestSellingProductsAsync(int top = 5)
        {
            var query = _unitOfWork.BillDetailsRepo.Query()
        .Where(bd => bd.ItemType == ItemType.Product); // ✅ Lọc sản phẩm

           

            return await query
                .GroupBy(bd => new { bd.ItemsName, bd.ImageUrl })
                .Select(g => new ChartItemDto
                {
                    Label = g.Key.ItemsName,
                    Value = g.Sum(bd => bd.Quantity),
                    ImageUrl = g.Key.ImageUrl ?? "/images/default.png"
                })
                .OrderByDescending(x => x.Value)
                .Take(top)
                .ToListAsync();
        }

        public async Task<int> GetCancelledOrdersCountAsync()
        {
            return await _unitOfWork.BillRepo.Query()
        .Where(b => b.Status == StatusOrder.Cancelled)
        .CountAsync();
        }

        public BillDangXuLyDto GetBillDangXuLy()
        {
            var bill = _unitOfWork.BillRepo.FindWhere(x => x.Status != StatusOrder.Completed && x.Status != StatusOrder.Cancelled);
            var Tong = bill.Count();
            var ChoXacNhan = bill.Where(x => x.Status == StatusOrder.Pending).Count();
            var DaXacNhan = bill.Where(x => x.Status == StatusOrder.Confirmed).Count();
            var DangGiaoHang = bill.Where(x => x.Status == StatusOrder.Shipping).Count();
            var DangChuanBi = bill.Where(x => x.Status == StatusOrder.DangChuanBi).Count();
            var ChoLayHang = bill.Where(x => x.Status == StatusOrder.ChoLayHang).Count();

            

            BillDangXuLyDto BillDangXuLyDto = new BillDangXuLyDto()
            {
                Tong = Tong,
                ChoXacNhan = ChoXacNhan,
                DangGiaoHang = DangGiaoHang,
                DaXacNhan = DaXacNhan,
                DangChuanBi= DangChuanBi,
                ChoLayHang =ChoLayHang
            };
            return BillDangXuLyDto;
        }
    }
}