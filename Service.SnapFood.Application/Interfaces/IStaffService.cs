using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IStaffService
    {
        DataTableJson GetPaged(BaseQuery query);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(UserDto item);
        Task<bool> UpdateAsync(Guid id, UserDto item);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ApproveAsync(Guid id);
        Task<bool> RejectAsync(Guid id);
        AuthResponseDto? Login(LoginDto loginDto);
    }
}