using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IPromotionService
    {
        DataTableJson GetPaged(BaseQuery query);
        List<PromotionDto> GetAll();
        Task<PromotionDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(PromotionDto item);
        Task<bool> UpdateAsync(Guid id, PromotionDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
