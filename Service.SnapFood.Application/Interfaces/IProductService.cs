using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Query;
using Service.SnapFood.Share.Model.Commons;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IProductService
    {
        DataTableJson GetPaged(ProductQuery query);
        Task<List<Product>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ProductDto item);
        Task<bool> UpdateAsync(Guid id, ProductDto item);

        Task<bool> DeleteAsync(Guid id);
        Dtos.StringContent CheckApprove(Guid id);
        Task<bool> ApproveAsync(Guid id);
        int CheckRejectAsync(Guid id);

        Task<bool> RejectAsync(Guid id);

    }
}
