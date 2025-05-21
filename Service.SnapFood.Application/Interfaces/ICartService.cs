using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartByIdUserAsync(Guid userId);
        Task AddComboToCartAsync(Guid userId, Guid comboId, int quantity);
        Task AddProductToCartAsync(Guid userId, Guid productId, int quantity);
        Task RemoveCartItem(Guid cartItemId);
        Task UpdateCartItem(Guid cartItemId, int quantity);
        Task ClearCart(Guid cartId);
        Task CheckOut(CheckOutDto item);
    }
}
