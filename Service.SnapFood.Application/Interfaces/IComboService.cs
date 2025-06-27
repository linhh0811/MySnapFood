using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IComboService
    {
        DataTableJson GetPaged(ComboQuery query);
        Task<List<Combo>> GetAllAsync();
        Task<ComboDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ComboDto item);
        Task<bool> UpdateAsync(Guid id, ComboDto item);

        Task<bool> DeleteAsync(Guid id);
        Dtos.StringContent CheckApproveAsync(Guid id);

        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
