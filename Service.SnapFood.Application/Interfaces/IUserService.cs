using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IUserService
    {
        DataTableJson GetPaged(BaseQuery query);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(UserDto item);
        Task<bool> UpdateAsync(Guid id, UserDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
        Task<User?> LoginAsync(LoginDto item);
    }
}