using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IStoreService
    {
        Task<StoreDto> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(StoreDto item);
        Task<bool> UpdateAsync(Guid id, StoreDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<StoreDto> GetStore();
    }
}
