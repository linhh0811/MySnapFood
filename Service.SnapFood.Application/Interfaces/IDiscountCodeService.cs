using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IDiscountCodeService
    {
        DataTableJson GetPaged(VoucherQuery query);
        Task<DiscountCodeDto?> GetByIdAsync(Guid id);
        Task<DiscountCodeDto> GetDiscountHoatDongById(Guid id);

        Task<Guid> CreateAsync(DiscountCodeDto item);
        Task<bool> UpdateAsync(Guid id, DiscountCodeDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);


    }
}
