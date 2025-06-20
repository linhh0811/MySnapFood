using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Model.Commons;
using Service.SnapFood.Share.Query;

namespace Service.SnapFood.Application.Interfaces
{
    public interface IUserService
    {
        DataTableJson GetPaged(BaseQuery query);
        Task<List<User>> GetAllAsync();
        Task<UserDto> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UserDto item);
        Task<AuthResponseDto?> LoginAsync(LoginDto item);
        Task<Guid> RegisterAsync(RegisterDto item);
    }
}