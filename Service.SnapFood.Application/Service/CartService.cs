using Microsoft.AspNetCore.Http;
using Service.SnapFood.Application.Dtos;
using Service.SnapFood.Application.Interfaces;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Enums;
using Service.SnapFood.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.SnapFood.Share.Model.SQL;

namespace Service.SnapFood.Application.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddComboToCartAsync(AddComboToCartDto item)
        {
            var cart = await GetOrCreateCartAsync(item.UserId);
            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);
            if (combo == null)
            {
                throw new Exception("Combo không còn tồn tại trên hệ thống");
            }

            if (combo.ModerationStatus != ModerationStatus.Approved)
            {
                throw new Exception("Combo chưa được duyệt");
            }

            var existingItem = cart.CartComboItems
                .FirstOrDefault(ci => ci.ComboId == item.ComboId && ci.SizeId == item.SizeId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _unitOfWork.CartComboItemRepo.Update(existingItem);// Sử dụng CartComboItemRepo
            }
            else
            {
                var cartItem = new CartComboItem
                {
                    CartId = cart.Id,
                    ComboId = item.ComboId,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity
                };
                await _unitOfWork.CartComboItemRepo.AddAsync(cartItem);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task AddProductToCartAsync(AddProductToCartDto item)
        {
            Console.WriteLine($"Processing cart for UserId: {item.UserId}, ProductId: {item.ProductId}, SizeId: {item.SizeId}, Quantity: {item.Quantity}");
            var cart = await GetOrCreateCartAsync(item.UserId);
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);
            if (product == null)
            {
                throw new Exception("Sản phẩm không còn tồn tại trên hệ thống");
            }

            if (product.ModerationStatus != ModerationStatus.Approved)
            {
                throw new Exception("Sản phẩm chưa được duyệt");
            }

            // Kiểm tra SizeId có hợp lệ không (giả sử có bảng Sizes và liên kết với Product)
            if (item.SizeId.HasValue && item.SizeId != Guid.Empty)
            {
                var size = await _unitOfWork.SizesRepo.GetByIdAsync(item.SizeId.Value);
                if (size == null)
                {
                    throw new Exception("Kích thước không hợp lệ");
                }
            }



            var existingItem = cart.CartProductItems
                .FirstOrDefault(ci => ci.ProductId == item.ProductId && ci.SizeId == item.SizeId);

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _unitOfWork.CartItemRepo.Update(existingItem);
            }
            else
            {
                var cartItem = new CartProductItem
                {
                    CartId = cart.Id,
                    ProductId = item.ProductId,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity
                };
                await _unitOfWork.CartItemRepo.AddAsync(cartItem);
            }

            try
            {
                await _unitOfWork.CompleteAsync();
                Console.WriteLine("Cart updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save cart: {ex}");
                throw;
            }
        }
        public async Task ClearCart(Guid cartId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsync(cartId);
            if (cart == null)
            {
                throw new Exception("Giỏ hàng không tồn tại");
            }

            foreach (var item in cart.CartProductItems.ToList())
            {
                _unitOfWork.CartItemRepo.Delete(item);
            }

            foreach (var item in cart.CartComboItems.ToList())
            {
                _unitOfWork.CartComboItemRepo.Delete(item);
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<CartDto> GetCartByIdUserAsync(Guid userId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartProductItems = new List<CartProductItem>(), CartComboItems = new List<CartComboItem>() };
                await _unitOfWork.CartRepo.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
            }

            var cartDto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartProductItems = cart.CartProductItems.Select(ci => new CartProductItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    SizeId = ci.SizeId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product.ProductName,
                    SizeName = ci.Size != null ? ci.Size.SizeName : "",
                    ImageUrl = ci.Product.ImageUrl,
                    Price = ci.Product.BasePrice
                }).ToList(),
                CartComboItems = cart.CartComboItems.Select(ci => new CartComboItemDto
                {
                    Id = ci.Id,
                    ComboId = ci.ComboId,
                    SizeId = ci.SizeId,
                    Quantity = ci.Quantity,
                    ComboName = ci.Combo.ComboName,
                    ImageUrl = ci.Combo.ImageUrl,
                    Price = ci.Combo.BasePrice,
                    ComboProductItems = ci.ComboProductItems.Select(cpi => new ComboProductItemDto
                    {
                        ProductId = cpi.ProductId,
                        SizeId = cpi.SizeId,
                        Quantity = cpi.Quantity,
                        ProductName = cpi.Product.ProductName,
                        SizeName = cpi.Size.SizeName
                    }).ToList()
                }).ToList()
            };

            cartDto.TotalQuantity = cartDto.CartProductItems.Sum(p => p.Quantity) + cartDto.CartComboItems.Sum(c => c.Quantity);
            cartDto.TotalPrice = cartDto.CartProductItems.Sum(p => p.Quantity * p.Price) + cartDto.CartComboItems.Sum(c => c.Quantity * c.Price);

            return cartDto;
        }

        public async Task RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _unitOfWork.CartItemRepo.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Mục trong giỏ hàng không tồn tại");
            }

            _unitOfWork.CartItemRepo.Delete(cartItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateCartItemAsync(Guid cartItemId, int quantity)
        {
            var cartItem = await _unitOfWork.CartItemRepo.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Mục trong giỏ hàng không tồn tại");
            }

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

        public async Task CheckOut(CheckOutDto item)
        {
            // Giữ nguyên mã cũ như yêu cầu
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(item.UserId);
            if (cart == null || (!cart.CartProductItems.Any() && !cart.CartComboItems.Any()))
            {
                throw new Exception("Giỏ hàng trống");
            }

            var bill = new Bill
            {
                UserId = item.UserId,
                TotalAmount = 0,
                BillDetails = new List<BillDetails>()
            };

            foreach (var cartItem in cart.CartProductItems)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                if (product == null) continue;

                bill.TotalAmount += product.BasePrice * cartItem.Quantity;

                var billDetail = new BillDetails
                {
                    BillId = bill.Id,
                    ItemType = ItemType.Product,
                    ItemsName = product.ProductName,
                    Quantity = cartItem.Quantity,
                    Price = product.BasePrice,
                    PriceEndow = 0
                };
                bill.BillDetails.Add(billDetail);
            }

            foreach (var cartItem in cart.CartComboItems)
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                if (combo == null) continue;

                bill.TotalAmount += combo.BasePrice * cartItem.Quantity;

                var billDetail = new BillDetails
                {
                    BillId = bill.Id,
                    ItemType = ItemType.Combo,
                    ItemsName = combo.ComboName,
                    Quantity = cartItem.Quantity,
                    Price = combo.BasePrice,
                    PriceEndow = 0
                };
                bill.BillDetails.Add(billDetail);
            }

            await _unitOfWork.BillRepo.AddAsync(bill);

            foreach (var itemToDelete in cart.CartProductItems.ToList())
            {
                _unitOfWork.CartItemRepo.Delete(itemToDelete);
            }

            foreach (var itemToDelete in cart.CartComboItems.ToList())
            {
                _unitOfWork.CartComboItemRepo.Delete(itemToDelete);
            }

            await _unitOfWork.CompleteAsync();
        }

        private async Task<Cart> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(userId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartProductItems = new List<CartProductItem>(),
                    CartComboItems = new List<CartComboItem>()
                };
                await _unitOfWork.CartRepo.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
            }
            return cart;
        }
        public async Task<int> GetCartQuantityAsync(Guid userId)
        {
            var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(userId);
            if (cart == null)
            {
                return 0; // Giỏ hàng trống hoặc không tồn tại
            }

            int totalQuantity = cart.CartProductItems.Sum(p => p.Quantity) + cart.CartComboItems.Sum(c => c.Quantity);
            return totalQuantity;
        }
    }
}