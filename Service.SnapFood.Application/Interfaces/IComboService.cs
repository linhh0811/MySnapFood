using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IComboService
    {
        DataTableJson GetPaged(BaseQuery query);
        Task<List<Combo>> GetAllAsync();
        Task<Combo> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ComboDto item);
        Task<bool> UpdateAsync(Guid id, ComboDto item);

        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
