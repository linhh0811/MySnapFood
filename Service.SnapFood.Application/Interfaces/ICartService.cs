using Service.SnapFood.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface ICartService
    {
        Task<Guid> AddCartNew(Guid UserId);
        Task AddComboToCartAsync(AddComboToCartDto item);
        Task AddProductToCartAsync(AddProductToCartDto item);
        Task CheckOut(CheckOutDto item);
        Task<Guid> CheckOutDatHangTaiQuay(CheckOutTaiQuayDto item);
        //Task ClearCart(Guid cartId);
        List<CartDto> GetListCartByUserId(Guid UserId);
        Task<CartDto> GetCartByIdUserAsync(Guid userId);
        Task<CartDto> GetCartByCartIdAsync(Guid cartId);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task RemoveCartAsync(Guid Id);
        Task UpdateCartItemAsync(Guid cartItemId, int quantity);
        Task RemoveComboItemAsync(Guid cartComboItemId);
        Task UpdateComboItemAsync(Guid cartComboItemId, int quantity);
        int GetCartQuantity(Guid userId);
        Task UpdateQuantity(QuantityInCartDto QuantityInCartDto);
        AddressDto GetAddressCheckout(Guid userId);
        Task<CartDto> GetCartByIdUserAsyncView(Guid userId);
        Task<CartDto> GetCartByCartIdAsyncView(Guid cartId);
    }
}
