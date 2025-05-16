using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IProductService
    {
        DataTableJson GetPaged(BaseQuery query);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(ProductDto item);
        Task<bool> UpdateAsync(Guid id,ProductDto item);

        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);

    }
}
