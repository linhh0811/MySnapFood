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
        private object query;

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

        public async Task<List<BillDetailsDto>> GetBillDetailsByBillIdAsync(Guid billId)
        {
            var allDetails = (await _unitOfWork.BillDetailsRepo.GetAllAsync())
                .Where(d => d.BillId == billId)
                .ToList();

            // Lấy danh sách ID của combo (ItemType == Combo)
            var comboIds = allDetails
                .Where(d => d.ItemType == ItemType.Combo)
                .Select(d => d.Id)
                .ToList();

            // Lấy toàn bộ combo items thuộc bill hiện tại
            var allComboItems = (await _unitOfWork.ComboItemsArchiveRepo.GetAllAsync())
                .Where(c => comboIds.Contains(c.BillDetailsId))
                .ToList();

            // Map comboId -> List<ComboItemsArchive>
            var comboItemGroups = allComboItems
                .GroupBy(c => c.BillDetailsId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<BillDetailsDto>();

            foreach (var detail in allDetails)
            {
                var detailDto = new BillDetailsDto
                {
                    Id = detail.Id,
                    ItemsName = detail.ItemsName,
                    ImageUrl = detail.ImageUrl,
                    Quantity = detail.Quantity,
                    Price = detail.Price,
                    PriceEndow = detail.PriceEndow,
                    ItemType = detail.ItemType,
                    Children = null
                };

                // Nếu là combo, thêm danh sách sản phẩm con
                if (detail.ItemType == ItemType.Combo && comboItemGroups.ContainsKey(detail.Id))
                {
                    detailDto.Children = comboItemGroups[detail.Id]
                        .Select(c => new BillDetailsDto
                        {
                            ItemsName = c.ProductName
                            //ImageUrl = c.ImageUrl,
                            //Quantity = c.Quantity,
                            //Price = c.Price,
                            //PriceEndow = 0,
                            //ItemType = ItemType.Product // hoặc ItemType khác tùy logic
                        })
                        .ToList();
                }

                result.Add(detailDto);
            }

            return result;
        }


        public async Task<List<BillDetails>> GetDetailsByBillIdAsync(Guid billId)
        {
            var results = _unitOfWork.BillDetailsRepo.FindWhere(x => x.BillId == billId);
            return results.ToList();
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
    }
}