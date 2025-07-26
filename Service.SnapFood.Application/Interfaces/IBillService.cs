using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IBillService
    {
        DataTableJson GetPage(BillQuery query);
        Task<List<Bill>> GetAllAsync();
        Task<Bill> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(BillDto item);
        Task<bool> UpdateAsync(Guid id, BillDto item);
        Task<bool> UpdateStatusAsync(Guid id, StatusOrder status);
        Task<BillDetailsDto?> GetDetailByIdAsync(Guid id);



        Task<List<BillDetailsDto>> GetBillDetailsByBillIdAsync(Guid billId);

        Task<List<BillDetails>> GetDetailsByBillIdAsync(Guid billId);

 
        Task<decimal> GetTotalRevenueAsync();

        Task<int> GetTotalInvoicesAsync();

        Task<List<DailyRevenueDto>> GetDailyRevenueAsync(DateTime fromDate, DateTime toDate);



        Task<List<ChartItemDto>> GetWeeklyRevenueAsync(DateTime? from = null, DateTime? to = null);
        Task<List<ChartItemDto>> GetMonthlyRevenueAsync(DateTime? from = null, DateTime? to = null);

        
        Task<List<ChartItemDto>> GetTopBestSellingProductsAsync( int top = 5);
        Task<List<ChartItemDto>> GetTopBestSellingCombosAsync( int top = 5);

        Task<int> GetCancelledOrdersCountAsync();

    }
}