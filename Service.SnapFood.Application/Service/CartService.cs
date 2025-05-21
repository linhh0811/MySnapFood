using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.SnapFood.Application.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddComboToCartAsync(Guid userId, Guid comboId, int quantity)
        {
            var cart = await GetCartByIdUserAsync(userId);

            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(comboId);
            if (combo == null)
                throw new Exception("Combo not found");

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ItemType == ItemType.Combo && ci.Combo != null && ci.Combo.Id == comboId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _unitOfWork.CartItemRepo.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ItemType = ItemType.Combo,
                    Quantity = quantity
                };
                await _unitOfWork.CartItemRepo.AddAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync();
        }
        public async Task AddProductToCartAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartByIdUserAsync(userId);

            var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId)
                          ?? throw new KeyNotFoundException("Product not found");

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ItemType == ItemType.Product && ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                _unitOfWork.CartItemRepo.Update(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    ItemType = ItemType.Product,
                    Quantity = quantity
                };

                await _unitOfWork.CartItemRepo.AddAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync();
        }



        public async Task CheckOut(CheckOutDto item)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(item.UserId);
            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty");

            var bill = new Bill
            {
                UserId = item.UserId,
                TotalAmount = 0,
                BillDetails = new List<BillDetails>()
            };

            foreach (var cartItem in cart.CartItems)
            {
                decimal price = 0;
                string itemName = "";

                if (cartItem.ProductId != Guid.Empty)
                {
                    var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                    if (product == null) continue;
                    price = product.BasePrice;
                    itemName = product.ProductName;
                }
                else if (cartItem.ItemType == ItemType.Combo && cartItem.Combo != null)
                {
                    var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.Combo.Id);
                    if (combo == null) continue;
                    price = combo.BasePrice;
                    itemName = combo.ComboName;
                }

                bill.TotalAmount += price * cartItem.Quantity;

                var billDetail = new BillDetails
                {
                    BillId = bill.Id,
                    ItemType = cartItem.ItemType,
                    ItemsName = itemName,
                    Quantity = cartItem.Quantity,
                    Price = price,
                    PriceEndow = 0
                };

                bill.BillDetails.Add(billDetail);
            }

            await _unitOfWork.BillRepo.AddAsync(bill);

            foreach (var itemToDelete in cart.CartItems.ToList())
            {
                _unitOfWork.CartItemRepo.Delete(itemToDelete);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task ClearCart(Guid cartId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsync(cartId);
            if (cart == null)
                throw new Exception("Cart not found");

            foreach (var item in cart.CartItems.ToList())
            {
                _unitOfWork.CartItemRepo.Delete(item);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<Cart?> GetCartByIdUserAsync(Guid userId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };

                await _unitOfWork.CartRepo.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
            }

            return cart;
        }

        public async Task RemoveCartItem(Guid cartItemId)
        {
            var cartItem = await _unitOfWork.CartItemRepo.GetByIdAsync(cartItemId);
            if (cartItem == null)
                throw new Exception("Cart item not found");

            _unitOfWork.CartItemRepo.Delete(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCartItem(Guid cartItemId, int quantity)
        {
            var cartItem = await _unitOfWork.CartItemRepo.GetByIdAsync(cartItemId);
            if (cartItem == null)
                throw new Exception("Cart item not found");

            if (quantity <= 0)
            {
                _unitOfWork.CartItemRepo.Delete(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
                _unitOfWork.CartItemRepo.Update(cartItem);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}
