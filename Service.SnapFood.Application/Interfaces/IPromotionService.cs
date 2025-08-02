using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IPromotionService
    {
        DataTableJson GetPaged(PromotionQuery query);
        int GetPromotionActivateCount();

        List<PromotionDto> GetPromotionActivate();
        Task<PromotionDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(PromotionDto item);
        Task<bool> UpdateAsync(Guid id, PromotionDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
