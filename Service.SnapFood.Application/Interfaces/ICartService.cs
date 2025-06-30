using Service.SnapFood.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface ICartService
    {
        Task AddComboToCartAsync(AddComboToCartDto item);
        Task AddProductToCartAsync(AddProductToCartDto item);
        Task CheckOut(CheckOutDto item);
        //Task ClearCart(Guid cartId);
        Task<CartDto> GetCartByIdUserAsync(Guid userId);
        Task RemoveCartItemAsync(Guid cartItemId);
        Task UpdateCartItemAsync(Guid cartItemId, int quantity);
        Task RemoveComboItemAsync(Guid cartComboItemId);
        Task UpdateComboItemAsync(Guid cartComboItemId, int quantity);
        //Task<int> GetCartQuantityAsync(Guid userId);
        Task UpdateQuantity(QuantityInCartDto QuantityInCartDto);
        AddressDto GetAddressCheckout(Guid userId);
    }
}
