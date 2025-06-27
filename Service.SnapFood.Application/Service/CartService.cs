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
                throw new Exception("Combo không còn tồn tại trên hệ thống");

            if (combo.ModerationStatus != ModerationStatus.Approved)
                throw new Exception("Combo chưa được duyệt");

            var comboProducts = _unitOfWork.ProductComboRepo.FindWhere(pc => pc.ComboId == item.ComboId).ToList();
            if (!comboProducts.Any())
                throw new Exception("Combo không có sản phẩm nào");

            // Chuyển đổi List<ComboProductItemDto> sang List<ComboProductItem>
            var productSizesEntities = item.ProductSizes.Select(ps => new ComboProductItem
            {
                ProductId = ps.ProductId,
                SizeId = ps.SizeId
            }).ToList();

            // Chuẩn hóa danh sách ProductSizes để đảm bảo đủ kích thước cho từng sản phẩm
            var productSizes = EnsureProductSizes(productSizesEntities, comboProducts);

            // K kiểm tra combo đã tồn tại trong giỏ hàng
            var existingComboItem = cart.CartComboItems.FirstOrDefault(ci =>
                ci.ComboId == item.ComboId &&
                AreComboItemsIdentical(ci, productSizes));

            if (existingComboItem != null)
            {
                // Tăng số lượng nếu combo trùng
                existingComboItem.Quantity += item.Quantity;
                _unitOfWork.CartComboItemRepo.Update(existingComboItem);
            }
            else
            {
                // Tạo combo mới
                var cartComboItem = new CartComboItem
                {
                    CartId = cart.Id,
                    ComboId = item.ComboId,
                    Quantity = item.Quantity,
                    ComboProductItems = new List<ComboProductItem>()
                };

                int sizeIndex = 0;
                foreach (var comboProduct in comboProducts)
                {
                    for (int i = 0; i < comboProduct.Quantity; i++)
                    {
                        if (sizeIndex < productSizes.Count)
                        {
                            var ps = productSizes[sizeIndex];
                            if (ps.ProductId != comboProduct.ProductId)
                                throw new Exception("Danh sách kích thước không khớp với sản phẩm trong combo.");

                            Guid? sizeIdToUse = ps.SizeId.HasValue && ps.SizeId != Guid.Empty ? ps.SizeId : await GetDefaultSize(comboProduct.ProductId);
                            ValidateSize(sizeIdToUse);

                            cartComboItem.ComboProductItems.Add(new ComboProductItem
                            {
                                ProductId = comboProduct.ProductId,
                                SizeId = sizeIdToUse,
                                Quantity = 1, // Mỗi ComboProductItem đại diện cho một đơn vị sản phẩm
                                CartComboId = cartComboItem.Id
                            });
                            sizeIndex++;
                        }
                        else
                        {
                            throw new Exception("Không đủ kích thước cho các sản phẩm trong combo.");
                        }
                    }
                }

                await _unitOfWork.CartComboItemRepo.AddAsync(cartComboItem);
            }

            await _unitOfWork.CompleteAsync();
        }

        private List<ComboProductItem> EnsureProductSizes(List<ComboProductItem> providedSizes, List<ProductCombo> comboProducts)
        {
            var result = new List<ComboProductItem>();
            var providedDict = providedSizes.GroupBy(ps => ps.ProductId).ToDictionary(g => g.Key, g => g.ToList());
            int requiredCount = comboProducts.Sum(pc => pc.Quantity);

            foreach (var cp in comboProducts)
            {
                for (int i = 0; i < cp.Quantity; i++)
                {
                    if (providedDict.TryGetValue(cp.ProductId, out var sizes) && sizes.Any())
                    {
                        var size = sizes.First();
                        result.Add(new ComboProductItem { ProductId = cp.ProductId, SizeId = size.SizeId });
                        sizes.RemoveAt(0);
                    }
                    else
                    {
                        // Nếu không có size được cung cấp, sẽ lấy mặc định sau
                        result.Add(new ComboProductItem { ProductId = cp.ProductId, SizeId = null });
                    }
                }
            }

            return result;
        }

        private async Task<Guid?> GetDefaultSize(Guid productId)
        {
            var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId);
            if (product == null || !product.SizeId.HasValue) return null;

            var sizes = _unitOfWork.SizesRepo.FindWhere(s => s.ParentId == product.SizeId && s.ModerationStatus == ModerationStatus.Approved)
                .OrderBy(s => s.DisplayOrder).ToList();
            return sizes.Any() ? sizes.First().Id : product.SizeId;
        }

        private void ValidateSize(Guid? sizeId)
        {
            if (sizeId.HasValue && sizeId != Guid.Empty)
            {
                var size = _unitOfWork.SizesRepo.GetById(sizeId.Value);
                if (size == null)
                    throw new Exception($"SizeId {sizeId} không tồn tại trong hệ thống.");
            }
        }

        private bool AreComboItemsIdentical(CartComboItem existingItem, List<ComboProductItem> newProductSizes)
        {
            var existingSizes = existingItem.ComboProductItems
                .Select(cp => new { cp.ProductId, cp.SizeId })
                .OrderBy(x => x.ProductId).ThenBy(x => x.SizeId)
                .ToList();

            var newSizes = newProductSizes
                .Select(ps => new { ps.ProductId, SizeId = ps.SizeId ?? Guid.Empty })
                .OrderBy(x => x.ProductId).ThenBy(x => x.SizeId)
                .ToList();

            if (existingSizes.Count != newSizes.Count) return false;

            return existingSizes.Zip(newSizes, (e, n) => e.ProductId == n.ProductId && e.SizeId == n.SizeId).All(x => x);
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
                        SizeName = cpi.Size?.SizeName ?? ""
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
        public async Task RemoveComboItemAsync(Guid cartComboItemId)
        {
            var cartComboItem = await _unitOfWork.CartComboItemRepo.GetByIdAsync(cartComboItemId);
            if (cartComboItem == null)
            {
                throw new Exception("Combo item not found.");
            }

            _unitOfWork.CartComboItemRepo.Delete(cartComboItem);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateComboItemAsync(Guid cartComboItemId, int quantity)
        {
            var cartComboItem = await _unitOfWork.CartComboItemRepo.GetByIdAsync(cartComboItemId);
            if (cartComboItem == null)
            {
                throw new Exception("Combo item not found.");
            }

            if (quantity <= 0)
            {
                _unitOfWork.CartComboItemRepo.Delete(cartComboItem);
            }
            else
            {
                cartComboItem.Quantity = quantity;
                _unitOfWork.CartComboItemRepo.Update(cartComboItem);
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