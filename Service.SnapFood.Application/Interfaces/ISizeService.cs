using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface ISizeService
    {
        Task<List<Sizes>> GetAllAsync();
        List<Sizes> GetSizeSelect();

        Task<List<SizeTreeDto>> GetSizeTreeAsync();
        Task<Sizes> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(SizeDto item);
        Task<bool> UpdateAsync(Guid id, SizeDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
    }
}
