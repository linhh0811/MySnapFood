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
using System.Net;

namespace Service.SnapFood.Application.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CartDto> GetListCartByUserId(Guid UserId)
        {
            var Carts = _unitOfWork.CartRepo.FindWhere(x => x.UserId == UserId);
            var CartDto = Carts.Select(x => new CartDto()
            {
                Id = x.Id,
                UserId = x.UserId
            });
            return CartDto.ToList();
        }

        public async Task<Guid> AddCartNew(Guid UserId)
        {
            Cart cartNew = new Cart();
            cartNew.UserId = UserId;

            _unitOfWork.CartRepo.Add(cartNew);
            await _unitOfWork.CompleteAsync();

            return cartNew.Id;
        }

        public async Task AddComboToCartAsync(AddComboToCartDto item)
        {
            Guid cartId = Guid.Empty;
            if (item.CartId == Guid.Empty)
            {
                var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
                if (cart is not null)
                {
                    cartId = cart.Id;
                }
                else
                {
                    throw new Exception("Không lấy được thông tin giỏ hàng");
                }

            }
            else
            {
                cartId = item.CartId;
            }


            if (cartId != Guid.Empty)
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);

                if (combo == null)
                    throw new Exception("Combo không còn tồn tại trên hệ thống");

                if (combo.ModerationStatus != ModerationStatus.Approved)
                    throw new Exception("Combo chưa được duyệt");

                var comboProducts = _unitOfWork.ProductComboRepo.FindWhere(pc => pc.ComboId == item.ComboId).ToList();
                if (!comboProducts.Any())
                    throw new Exception("Combo không có sản phẩm nào");

                var cartComboItemIsExist = _unitOfWork.CartComboItemRepo.FirstOrDefault(x => x.ComboId == item.ComboId && x.CartId == cartId);



                if (cartComboItemIsExist is null)
                {
                    CartComboItem cartComboItem = new CartComboItem()
                    {
                        CartId = cartId,
                        ComboId = item.ComboId,
                        Quantity = item.Quantity
                    };

                    _unitOfWork.CartComboItemRepo.Add(cartComboItem);
                    await _unitOfWork.CompleteAsync();

                    var comboProductItems = item.ProductSizes.Select(x => new ComboProductItem()
                    {
                        CartComboId = cartComboItem.Id,
                        SizeId = x.SizeId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity
                    });
                    _unitOfWork.ComboProductItemRepository.AddRange(comboProductItems);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {
                    var ComboProductItems = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cartComboItemIsExist.Id).ToList();
                    var CheckItem = CheckComboProductItemsIsExist(ComboProductItems, item.ProductSizes);
                    if (CheckItem)
                    {
                        cartComboItemIsExist.Quantity = cartComboItemIsExist.Quantity + item.Quantity;
                        _unitOfWork.CartComboItemRepo.Update(cartComboItemIsExist);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        CartComboItem cartComboItem = new CartComboItem()
                        {
                            CartId = cartId,
                            ComboId = item.ComboId,
                            Quantity = item.Quantity
                        };

                        _unitOfWork.CartComboItemRepo.Add(cartComboItem);
                        await _unitOfWork.CompleteAsync();

                        var comboProductItems = item.ProductSizes.Select(x => new ComboProductItem()
                        {
                            CartComboId = cartComboItem.Id,
                            SizeId = x.SizeId,
                            ProductId = x.ProductId,
                            Quantity = x.Quantity
                        });
                        _unitOfWork.ComboProductItemRepository.AddRange(comboProductItems);
                        await _unitOfWork.CompleteAsync();
                    }
                }


            }
            else
            {
                throw new Exception("Không tìm thấy giỏ hàng");
            }

        }

        private bool CheckComboProductItemsIsExist(List<ComboProductItem> ComboProductItem, List<ComboProductItemDto> ComboProductItemDto)
        {
            foreach (var item in ComboProductItem)
            {
                var ItemCheck = ComboProductItemDto.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (ItemCheck is not null)
                {
                    if (ItemCheck.SizeId != item.SizeId)
                    {
                        return false;
                    }

                }
            }
            return true;
        }


        public async Task AddProductToCartAsync(AddProductToCartDto item)
        {
            Guid cartId = Guid.Empty;
            if (item.CartId==Guid.Empty)
            {
                var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
                if (cart is not null)
                {
                    cartId = cart.Id;
                }
                else
                {
                    throw new Exception("Không lấy được thông tin giỏ hàng");
                }

            }
            else
            {
                cartId = item.CartId;
            }

            if (cartId !=Guid.Empty)
            {
                if (item.SizeId == Guid.Empty || item.SizeId == null)
                {
                    var cartProductItemIsExist = _unitOfWork.CartItemRepo.FirstOrDefault(x => x.ProductId == item.ProductId && x.CartId == cartId);
                    if (cartProductItemIsExist is not null)
                    {
                        cartProductItemIsExist.Quantity = cartProductItemIsExist.Quantity + item.Quantity;
                        _unitOfWork.CartItemRepo.Update(cartProductItemIsExist);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        CartProductItem cartProductItem = new CartProductItem();
                        cartProductItem.CartId = cartId;
                        if (item.SizeId != Guid.Empty)
                        {
                            cartProductItem.SizeId = item.SizeId;
                        }
                        cartProductItem.ProductId = item.ProductId;
                        cartProductItem.Quantity = item.Quantity;

                        _unitOfWork.CartItemRepo.Add(cartProductItem);
                        await _unitOfWork.CompleteAsync();
                    }
                }
                else
                {
                    var cartProductItemIsExist = _unitOfWork.CartItemRepo.FirstOrDefault(x => x.ProductId == item.ProductId && x.SizeId == item.SizeId && x.CartId == cartId);
                    if (cartProductItemIsExist is not null)
                    {
                        cartProductItemIsExist.Quantity = cartProductItemIsExist.Quantity + item.Quantity;
                        _unitOfWork.CartItemRepo.Update(cartProductItemIsExist);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        CartProductItem cartProductItem = new CartProductItem();
                        cartProductItem.CartId = cartId;
                        if (item.SizeId != Guid.Empty)
                        {
                            cartProductItem.SizeId = item.SizeId;
                        }
                        cartProductItem.ProductId = item.ProductId;
                        cartProductItem.Quantity = item.Quantity;
                        cartProductItem.SizeId = item.SizeId;

                        _unitOfWork.CartItemRepo.Add(cartProductItem);
                        await _unitOfWork.CompleteAsync();
                    }
                }

            }
            else
            {
                throw new Exception("Không lấy được thông tin giỏ hàng");
            }
        }
        //public async Task ClearCart(Guid cartId)
        //{
        //    var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsync(cartId);
        //    if (cart == null)
        //    {
        //        throw new Exception("Giỏ hàng không tồn tại");
        //    }

        //    foreach (var item in cart.CartProductItems.ToList())
        //    {
        //        _unitOfWork.CartItemRepo.Delete(item);
        //    }

        //    foreach (var item in cart.CartComboItems.ToList())
        //    {
        //        _unitOfWork.CartComboItemRepo.Delete(item);
        //    }

        //    await _unitOfWork.CompleteAsync();
        //}

        public async Task<CartDto> GetCartByCartIdAsync(Guid cartId)
        {

            var cart =await _unitOfWork.CartRepo.GetByIdAsync(cartId);
            if (cart is null)
            {
                throw new Exception("Không lấy được thông tin giỏ hàng");

            }

            var cartDto = new CartDto
            {
                Id = cartId,
                UserId = cart.UserId,
            };

            var CartProductItem = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id);
            var CartComboItem = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id);

            List<CartItemDto> CartItems = new List<CartItemDto>();
            foreach (var item in CartProductItem)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);
                if (product != null && product.ModerationStatus == ModerationStatus.Approved)
                {
                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(item.SizeId ?? Guid.Empty);
                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ProductId,
                        ItemName = product.ProductName,
                        SizeName = size?.SizeName ?? "Tiêu chuẩn",
                        BasePrice = product.BasePrice + (size?.AdditionalPrice ?? 0),
                        PriceEndown = GetProductPriceEndown(item.ProductId, product.BasePrice, (size?.AdditionalPrice ?? 0)),
                        Quantity = item.Quantity,
                        ImageUrl = product.ImageUrl,
                        ItemType = ItemType.Product,
                        Created = item.Created,
                        ModerationStatus = product.ModerationStatus

                    });
                }
            }

            foreach (var item in CartComboItem)
            {
                var ComboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == item.Id);

                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);
                if (combo != null && combo.ModerationStatus == ModerationStatus.Approved)
                {
                    decimal priceSize = 0;
                    foreach (var c in ComboProductItem)
                    {
                        var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                        if (size is not null)
                        {
                            priceSize += size.AdditionalPrice;
                        }

                    }

                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ComboId,
                        ItemName = combo.ComboName,
                        BasePrice = combo.BasePrice + priceSize,
                        PriceEndown = GetComboPriceEndown(item.ComboId, combo.BasePrice, priceSize),
                        Quantity = item.Quantity,
                        ImageUrl = combo.ImageUrl,
                        ItemType = ItemType.Combo,
                        ComboItems = _unitOfWork.ProductComboRepo
                                .FindWhere(x => x.ComboId == item.ComboId)
                                .Select(x => new ComboProductDto
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId)?.ProductName,
                                    SizeName = GetSizeName(item.Id, x.ProductId)
                                }).ToList(),
                        Created = item.Created,
                        ModerationStatus = combo.ModerationStatus

                    });
                }
            }
            cartDto.CartItems = CartItems.OrderByDescending(x => x.Created).ToList();

            return cartDto;
        }

        public async Task<CartDto> GetCartByCartIdAsyncView(Guid cartId)
        {

            var cart = await _unitOfWork.CartRepo.GetByIdAsync(cartId);
            if (cart is null)
            {
                throw new Exception("Không lấy được thông tin giỏ hàng");

            }

            var cartDto = new CartDto
            {
                Id = cartId,
                UserId = cart.UserId,
            };

            var CartProductItem = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id);
            var CartComboItem = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id);

            List<CartItemDto> CartItems = new List<CartItemDto>();
            foreach (var item in CartProductItem)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);
                if (product != null/* && product.ModerationStatus == ModerationStatus.Approved*/)
                {
                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(item.SizeId ?? Guid.Empty);
                    ModerationStatus ModerationStatus = product.ModerationStatus;
                    if (size?.ModerationStatus == ModerationStatus.Rejected)
                    {
                        ModerationStatus = ModerationStatus.Rejected;
                    }
                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ProductId,
                        ItemName = product.ProductName,
                        SizeName = size?.SizeName ?? "Tiêu chuẩn",
                        BasePrice = product.BasePrice + (size?.AdditionalPrice ?? 0),
                        PriceEndown = GetProductPriceEndown(item.ProductId, product.BasePrice, (size?.AdditionalPrice ?? 0)),
                        Quantity = item.Quantity,
                        ImageUrl = product.ImageUrl,
                        ItemType = ItemType.Product,
                        Created = item.Created,
                        ModerationStatus= ModerationStatus
                    });
                }
            }

            foreach (var item in CartComboItem)
            {
                var ComboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == item.Id);

                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);
                if (combo != null /*&& combo.ModerationStatus == ModerationStatus.Approved*/)
                {
                    decimal priceSize = 0;
                    foreach (var c in ComboProductItem)
                    {
                        var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                        if (size is not null)
                        {
                            priceSize += size.AdditionalPrice;
                        }

                    }

                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ComboId,
                        ItemName = combo.ComboName,
                        BasePrice = combo.BasePrice + priceSize,
                        PriceEndown = GetComboPriceEndown(item.ComboId, combo.BasePrice, priceSize),
                        Quantity = item.Quantity,
                        ImageUrl = combo.ImageUrl,
                        ItemType = ItemType.Combo,
                        ComboItems = _unitOfWork.ProductComboRepo
                                .FindWhere(x => x.ComboId == item.ComboId)
                                .Select(x => new ComboProductDto
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId)?.ProductName,
                                    SizeName = GetSizeName(item.Id, x.ProductId)
                                }).ToList(),
                        Created = item.Created,
                        ModerationStatus = GetModerationStatus(item.Id, combo.ModerationStatus)

                    });
                }
            }
            cartDto.CartItems = CartItems.OrderByDescending(x => x.Created).ToList();

            return cartDto;
        }
        public async Task<CartDto> GetCartByIdUserAsync(Guid userId)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == userId);
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
            };

            var CartProductItem = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id);
            var CartComboItem = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id);

            List<CartItemDto> CartItems = new List<CartItemDto>();
            foreach (var item in CartProductItem)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);
                if (product != null )
                {
                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(item.SizeId ?? Guid.Empty);
                    ModerationStatus ModerationStatus = product.ModerationStatus;
                    if (size?.ModerationStatus == ModerationStatus.Rejected)
                    {
                        ModerationStatus = ModerationStatus.Rejected;
                    }
                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ProductId,
                        ItemName = product.ProductName,
                        SizeName = size?.SizeName ?? "Tiêu chuẩn",
                        BasePrice = product.BasePrice + (size?.AdditionalPrice ?? 0),
                        PriceEndown = GetProductPriceEndown(item.ProductId, product.BasePrice, (size?.AdditionalPrice ?? 0)),
                        Quantity = item.Quantity,
                        ImageUrl = product.ImageUrl,
                        ItemType = ItemType.Product,
                        Created = item.Created,
                        ModerationStatus= ModerationStatus

                    });
                }
            }

            foreach (var item in CartComboItem)
            {
                var ComboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == item.Id);

                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);
                if (combo != null && combo.ModerationStatus == ModerationStatus.Approved)
                {
                    decimal priceSize = 0;
                    foreach (var c in ComboProductItem)
                    {
                        var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                        if (size is not null)
                        {
                            priceSize += size.AdditionalPrice;
                        }

                    }

                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ComboId,
                        ItemName = combo.ComboName,
                        BasePrice = combo.BasePrice + priceSize,
                        PriceEndown = GetComboPriceEndown(item.ComboId, combo.BasePrice, priceSize),
                        Quantity = item.Quantity,
                        ImageUrl = combo.ImageUrl,
                        ItemType = ItemType.Combo,
                        ComboItems = _unitOfWork.ProductComboRepo
                                .FindWhere(x => x.ComboId == item.ComboId)
                                .Select(x => new ComboProductDto
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId)?.ProductName,
                                    SizeName = GetSizeName(item.Id, x.ProductId)
                                }).ToList(),
                        Created = item.Created,
                        ModerationStatus = GetModerationStatus(item.Id, combo.ModerationStatus)

                    });
                }
            }
            cartDto.CartItems = CartItems.OrderByDescending(x => x.Created).ToList();

            return cartDto;
        }

        public async Task<CartDto> GetCartByIdUserAsyncView(Guid userId)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == userId);
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
            };

            var CartProductItem = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id);
            var CartComboItem = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id);

            List<CartItemDto> CartItems = new List<CartItemDto>();
            foreach (var item in CartProductItem)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(item.ProductId);
                if (product != null /*&& product.ModerationStatus == ModerationStatus.Approved*/)
                {
                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(item.SizeId ?? Guid.Empty);
                    ModerationStatus ModerationStatus = product.ModerationStatus;
                    if (size?.ModerationStatus==ModerationStatus.Rejected)
                    {
                        ModerationStatus = ModerationStatus.Rejected;
                    }
                    CartItems.Add(new CartItemDto 
                    {
                        Id = item.Id,
                        ItemId = item.ProductId,
                        ItemName = product.ProductName,
                        SizeName = size?.SizeName ?? "Tiêu chuẩn",
                        BasePrice = product.BasePrice + (size?.AdditionalPrice ?? 0),
                        PriceEndown = GetProductPriceEndown(item.ProductId, product.BasePrice, (size?.AdditionalPrice ?? 0)),
                        Quantity = item.Quantity,
                        ImageUrl = product.ImageUrl,
                        ItemType = ItemType.Product,
                        Created = item.Created,
                        ModerationStatus = ModerationStatus

                    });
                }
            }

            foreach (var item in CartComboItem)
            {
                var ComboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == item.Id);

                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);
                if (combo != null /*&& combo.ModerationStatus == ModerationStatus.Approved*/)
                {
                    decimal priceSize = 0;
                    foreach (var c in ComboProductItem)
                    {
                        var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                        if (size is not null)
                        {
                            priceSize += size.AdditionalPrice;
                        }

                    }

                    CartItems.Add(new CartItemDto
                    {
                        Id = item.Id,
                        ItemId = item.ComboId,
                        ItemName = combo.ComboName,
                        BasePrice = combo.BasePrice + priceSize,
                        PriceEndown = GetComboPriceEndown(item.ComboId, combo.BasePrice, priceSize),
                        Quantity = item.Quantity,
                        ImageUrl = combo.ImageUrl,
                        ItemType = ItemType.Combo,
                        ComboItems = _unitOfWork.ProductComboRepo
                                .FindWhere(x => x.ComboId == item.ComboId)
                                .Select(x => new ComboProductDto
                                {
                                    ProductId = x.ProductId,
                                    Quantity = x.Quantity,
                                    ProductName = x.Quantity + " " + _unitOfWork.ProductRepo.GetById(x.ProductId)?.ProductName,
                                    SizeName = GetSizeName(item.Id, x.ProductId)
                                }).ToList(),
                        Created = item.Created,
                        ModerationStatus = GetModerationStatus(item.Id, combo.ModerationStatus)

                    });
                }
            }
            cartDto.CartItems = CartItems.OrderByDescending(x => x.Created).ToList();

            return cartDto;
        }
        private string GetSizeName(Guid CartComboId, Guid ProductId)
        {
            var sizeId = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == CartComboId && x.ProductId == ProductId).First().SizeId;
            var sizeName = _unitOfWork.SizesRepo.GetById(sizeId ?? Guid.Empty)?.SizeName ?? "Tiêu chuẩn";
            return sizeName;

        }

        private ModerationStatus GetModerationStatus(Guid CartComboId,  ModerationStatus ComboModerationStatus)
        {
            var sizeIds = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == CartComboId).Select(x=>x.SizeId);
            foreach (var item in sizeIds)
            {
                var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == item && x.ModerationStatus == ModerationStatus.Rejected);
                if (size is not null)
                {
                    return ModerationStatus.Rejected;
                }
            }
            return ComboModerationStatus;

        }
        public async Task RemoveCartAsync(Guid Id)
        {
            var cart = await _unitOfWork.CartRepo.GetByIdAsync(Id);
            if (cart == null)
            {
                throw new Exception("Mục trong giỏ hàng không tồn tại");
            }

            _unitOfWork.CartRepo.Delete(cart);
            await _unitOfWork.CompleteAsync();
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

            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
            if (cart is not null)
            {
                try
                {


                    _unitOfWork.BeginTransaction();
                    var cartProductItems = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
                    var cartComboItems = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

                    var ProductIds = cartProductItems.Select(x => x.ProductId).ToList();
                    var ComboIds = cartComboItems.Select(x => x.ComboId).ToList();

                    foreach (var p in ProductIds)
                    {
                        var productCheck = _unitOfWork.ProductRepo.FindWhere(x => x.Id == p&&x.ModerationStatus==ModerationStatus.Rejected);
                        if(productCheck.Count()>0)
                        {
                            throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                        }
                    }

                    foreach (var c in ComboIds)
                    {
                        var comboCheck = _unitOfWork.ComboRepo.FindWhere(x => x.Id == c && x.ModerationStatus == ModerationStatus.Rejected);
                        if (comboCheck.Count() > 0)
                        {
                            throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                        }
                    }

                    if (item.DiscountCodeId != Guid.Empty)
                    {
                        var discount = _unitOfWork.DiscountCodeRepo.FindWhere(x => x.Id == item.DiscountCodeId && x.ModerationStatus == ModerationStatus.Rejected);
                        if (discount.Count() > 0)
                        {
                            throw new Exception("Mã giảm giá không hoạt động, vui lòng tải lại trang");

                        }
                    }


                    if (!cartProductItems.Any() && !cartComboItems.Any())
                    {
                        throw new Exception("Giỏ hàng trống");
                    }

                    bool isCoSPHoatDong = false;
                    foreach (var p in cartProductItems)
                    {
                        var product = await _unitOfWork.ProductRepo.GetByIdAsync(p.ProductId);

                        if (product is not null && product.ModerationStatus==ModerationStatus.Approved)
                        {
                            isCoSPHoatDong = true;
                        }

                    }
                    foreach (var c in cartComboItems)
                    {
                        var combo = await _unitOfWork.ComboRepo.GetByIdAsync(c.ComboId);

                        if (combo is not null && combo.ModerationStatus == ModerationStatus.Approved)
                        {
                            isCoSPHoatDong = true;
                        }
                    }

                    

                    if (!isCoSPHoatDong)
                    {
                        throw new Exception("Giỏ hàng trống");
                    }
                    var store = _unitOfWork.StoresRepo.FirstOrDefault(x => x.Status == Status.Activity);
                    if (store is not null)
                    {
                        var bill = new Bill
                        {
                            UserId = item.UserId,
                            BillCode = BillCodeGen(item.ReceivingType),
                            StoreId = store.Id,
                            TotalAmount = 0,
                            DiscountAmount=item.DiscountCodeValue,
                            BillDetails = new List<BillDetails>(),
                            Status = StatusOrder.Pending,
                            ReceivingType = item.ReceivingType,
                            PhuongThucDatHang= Share.Model.Enum.PhuongThucDatHangEnum.DatTaiWeb


                        };
                        _unitOfWork.BillRepo.Add(bill);
                        await _unitOfWork.CompleteAsync();

                        foreach (var cartItem in cartProductItems)
                        {
                            var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                           
                            var size = _unitOfWork.SizesRepo.GetById(cartItem.SizeId ?? Guid.Empty);
                            var sizeName = size?.SizeName ?? "Tiêu chuẩn";
                            if (product == null) continue;
                            if (product.ModerationStatus == ModerationStatus.Approved)
                            {
                                bill.TotalAmount += product.BasePrice * cartItem.Quantity;

                                var billDetail = new BillDetails
                                {
                                    BillId = bill.Id,
                                    ItemId = product.Id,
                                    ItemType = ItemType.Product,
                                    ItemsName = product.ProductName + " " + "(Size: " + sizeName + ")",
                                    ImageUrl = product.ImageUrl,
                                    Quantity = cartItem.Quantity,
                                    Price = product.BasePrice + (size?.AdditionalPrice ?? 0),
                                    PriceEndow = GetPriceEndown(cartItem.ProductId, product.BasePrice, size?.AdditionalPrice ?? 0),
                                };
                                bill.BillDetails.Add(billDetail);
                                _unitOfWork.BillDetailsRepo.Add(billDetail);
                                await _unitOfWork.CompleteAsync();
                            }
                           
                        }

                        foreach (var cartItem in cartComboItems)
                        {
                            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                            if (combo == null) continue;
                            if (combo.ModerationStatus==ModerationStatus.Approved)
                            {
                                decimal priceSize = 0;
                                var comboItems = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cartItem.Id).ToList();

                                foreach (var c in comboItems)
                                {
                                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                                    if (size is not null)
                                    {
                                        priceSize += size.AdditionalPrice;
                                    }

                                }

                                bill.TotalAmount += combo.BasePrice * cartItem.Quantity;

                                var billDetail = new BillDetails
                                {
                                    BillId = bill.Id,
                                    ItemId = combo.Id,
                                    ItemType = ItemType.Combo,
                                    ItemsName = combo.ComboName,
                                    ImageUrl = combo.ImageUrl,
                                    Quantity = cartItem.Quantity,
                                    Price = combo.BasePrice + priceSize,
                                    PriceEndow = GetPriceEndown(cartItem.ComboId, combo.BasePrice, priceSize),
                                };
                                bill.BillDetails.Add(billDetail);
                                _unitOfWork.BillDetailsRepo.Add(billDetail);
                                await _unitOfWork.CompleteAsync();

                                foreach (var i in comboItems)
                                {
                                    var product = await _unitOfWork.ProductRepo.GetByIdAsync(i.ProductId);
                                    var sizeName = _unitOfWork.SizesRepo.GetById(i.SizeId ?? Guid.Empty)?.SizeName ?? "Tiêu chuẩn";
                                    if (product is not null)
                                    {
                                        ComboItemsArchive ComboItemsArchive = new ComboItemsArchive()
                                        {
                                            BillDetailsId = billDetail.Id,
                                            ProductId = product.Id,
                                            ProductName = product.ProductName + " " + "(Size: " + sizeName + ")",
                                            Quantity = i.Quantity,
                                            ImageUrl = product.ImageUrl,
                                            Price = product.BasePrice,
                                        };
                                        _unitOfWork.ComboItemsArchiveRepo.Add(ComboItemsArchive);
                                        await _unitOfWork.CompleteAsync();
                                    }



                                }
                            }
                           
                        }
                       
                        

                        if (item.ReceivingType == ReceivingType.HomeDelivery)
                        {
                            var address = _unitOfWork.AddressRepo
                        .FirstOrDefault(x => x.UserId == item.UserId && x.AddressType == AddressType.Default);
                            if (address is not null)
                            {
                                BillDelivery billDelivery = new BillDelivery()
                                {
                                    BillId = bill.Id,
                                    ReceivingType = item.ReceivingType,
                                    ReceiverName = address.FullName,
                                    ReceiverPhone = address.NumberPhone,
                                    ReceiverAddress = address.FullAddress,
                                    Distance = item.KhoangCach,
                                    DeliveryFee = item.PhiGiaoHang,
                                };
                                _unitOfWork.BillDeliveryRepo.Add(billDelivery);
                                await _unitOfWork.CompleteAsync();
                            }

                        }
                        else if (item.ReceivingType == ReceivingType.PickUpAtStore)
                        {
                            BillDelivery billDelivery = new BillDelivery()
                            {
                                BillId = bill.Id,
                                ReceivingType = item.ReceivingType,
                                ReceiverName = item.ReceiverName,
                                ReceiverPhone = item.ReceiverPhone,
                                ReceiverAddress = "",
                                Distance = 0,
                                DeliveryFee = 0,
                            };
                            _unitOfWork.BillDeliveryRepo.Add(billDelivery);
                            await _unitOfWork.CompleteAsync();
                        }
                        BillNotes billNotes1 = new BillNotes()
                        {
                            BillId = bill.Id,
                            NoteType = NoteType.CustomerOrder,
                            NoteContent = "Đơn hàng đã được đặt tại website",
                            CreatedBy = Guid.Empty

                        };
                        _unitOfWork.BillNotesRepo.Add(billNotes1);
                        await _unitOfWork.CompleteAsync();

                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            BillNotes billNotes2 = new BillNotes()
                            {
                                BillId=bill.Id,
                                NoteType = NoteType.CustomerOrder,
                                NoteContent = "Ghi chú: "+item.Description,
                                CreatedBy = Guid.Empty

                            };

                            _unitOfWork.BillNotesRepo.Add(billNotes2);
                            await _unitOfWork.CompleteAsync();
                        }

                        if (item.DiscountCodeId!=Guid.Empty)
                        {
                            var discoutCode = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(item.DiscountCodeId);

                            if (discoutCode is not null&&discoutCode.ModerationStatus==ModerationStatus.Approved&&discoutCode.StartDate<=DateTime.Now&&DateTime.Now<=discoutCode.EndDate)
                            {
                                if (discoutCode.UsedCount<discoutCode.UsageLimit)
                                {
                                    discoutCode.UsedCount += 1;
                                    _unitOfWork.DiscountCodeRepo.Update(discoutCode);
                                    await _unitOfWork.CompleteAsync();
                                    var discoutCodeUse = new DiscountCodeUsage()
                                    {
                                        DiscountCodeId = discoutCode.Id,
                                        UserId = item.UserId,
                                        BillId = bill.Id
                                    };
                                    _unitOfWork.DiscountCodeUsageRepo.Add(discoutCodeUse);
                                    await _unitOfWork.CompleteAsync();
                                }
                                else
                                {
                                    throw new Exception("Mã giảm giá đã hết lượt sử dụng");
                                }
                            }
                        }
                        

                        var billDetails = _unitOfWork.BillDetailsRepo.FindWhere(x => x.BillId == bill.Id).ToList();

                        BillPayment BillPayment = new BillPayment()
                        {
                            BillId = bill.Id,
                            PaymentType = item.PaymentType,
                            Amount = billDetails.Where(x => x.PriceEndow > 0).Sum(p => p.Price * p.Quantity - p.PriceEndow * p.Quantity),
                        };
                        _unitOfWork.BillPaymentRepo.Add(BillPayment);
                        await _unitOfWork.CompleteAsync();

                        var billUpdate = await _unitOfWork.BillRepo.GetByIdAsync(bill.Id);
                        if (billUpdate is not null)
                        {
                            billUpdate.TotalAmount = billDetails.Sum(x => x.Price * x.Quantity);
                            billUpdate.TotalAmountEndow = billDetails.Where(x => x.PriceEndow > 0).Sum(p => p.Price * p.Quantity - p.PriceEndow * p.Quantity);

                            if (item.TongTienKhuyenMai != billUpdate.TotalAmountEndow)
                            {
                                throw new Exception("Khuyến mãi đã có sự thay đổi");
                            }
                            _unitOfWork.BillRepo.Update(billUpdate);
                        }

                    

                        _unitOfWork.CartItemRepo.DeleteRange(cartProductItems);
                        _unitOfWork.CartComboItemRepo.DeleteRange(cartComboItems);

                        await _unitOfWork.CompleteAsync();
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        throw new Exception("Cửa hàng tạm thời đóng cửa");
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy giỏ hàng");
            }

        }

        public async Task CheckOutValidateOnly(CheckOutDto item)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
            if (cart is null)
                throw new Exception("Không tìm thấy giỏ hàng");

            var cartProductItems = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
            var cartComboItems = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

            if (!cartProductItems.Any() && !cartComboItems.Any())
                throw new Exception("Giỏ hàng trống");

            // Kiểm tra sản phẩm bị từ chối
            var rejectedProduct = _unitOfWork.ProductRepo.FindWhere(x =>
                cartProductItems.Select(i => i.ProductId).Contains(x.Id)
                && x.ModerationStatus == ModerationStatus.Rejected
            );

            foreach (var cp in cartProductItems)
            {
                var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cp.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                if (size is not null)
                {
                    throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                }
            }

            foreach (var cp in cartComboItems)
            {
                var comboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cp.Id);
                foreach (var cpi in comboProductItem)
                {
                    var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cpi.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                    if (size is not null)
                    {
                        throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                    }
                }
                
            }

            if (rejectedProduct.Any())
                throw new Exception("Đơn hàng đã có sự thay đổi (sản phẩm hết hàng), vui lòng tải lại trang");

            // Kiểm tra combo bị từ chối
            var rejectedCombo = _unitOfWork.ComboRepo.FindWhere(x =>
                cartComboItems.Select(i => i.ComboId).Contains(x.Id)
                && x.ModerationStatus == ModerationStatus.Rejected
            );

            if (rejectedCombo.Any())
                throw new Exception("Đơn hàng đã có sự thay đổi (combo hết hàng), vui lòng tải lại trang");

            // Kiểm tra mã giảm giá bị từ chối
            if (item.DiscountCodeId != Guid.Empty)
            {
                var discount = _unitOfWork.DiscountCodeRepo.FindWhere(x =>
                    x.Id == item.DiscountCodeId &&
                    x.ModerationStatus == ModerationStatus.Rejected
                );

                if (discount.Any())
                    throw new Exception("Mã giảm giá không hoạt động, vui lòng tải lại trang");
            }

            // Kiểm tra có ít nhất 1 sản phẩm hoạt động
            bool isCoSPHoatDong = false;

            foreach (var p in cartProductItems)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(p.ProductId);
                if (product?.ModerationStatus == ModerationStatus.Approved)
                {
                    isCoSPHoatDong = true;
                    break;
                }
            }

            foreach (var c in cartComboItems)
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(c.ComboId);
                if (combo?.ModerationStatus == ModerationStatus.Approved)
                {
                    isCoSPHoatDong = true;
                    break;
                }
            }

            if (!isCoSPHoatDong)
                throw new Exception("Giỏ hàng trống (không có sản phẩm nào đang hoạt động)");

            // Kiểm tra khuyến mãi
            if (item.TongTienKhuyenMai > 0)
            {
                var comboItems = _unitOfWork.ComboProductItemRepository
                    .FindWhere(x => cartComboItems.Select(c => c.Id).Contains(x.CartComboId))
                    .ToList();

                decimal tongEndow = 0;

                foreach (var cartItem in cartProductItems)
                {
                    var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                    var size = _unitOfWork.SizesRepo.GetById(cartItem.SizeId ?? Guid.Empty);
                    if (product?.ModerationStatus == ModerationStatus.Approved)
                    {
                        var gia = product.BasePrice + (size?.AdditionalPrice ?? 0);
                        var priceEndow = GetPriceEndown(cartItem.ProductId, product.BasePrice, size?.AdditionalPrice ?? 0);
                        if (priceEndow>0)
                        {
                            tongEndow += (gia - priceEndow) * cartItem.Quantity;
                        }
                        
                    }
                }

                foreach (var cartItem in cartComboItems)
                {
                    var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                    if (combo?.ModerationStatus == ModerationStatus.Approved)
                    {
                        decimal priceSize = comboItems
                            .Where(x => x.CartComboId == cartItem.Id)
                            .Sum(x => _unitOfWork.SizesRepo.GetById(x.SizeId ?? Guid.Empty)?.AdditionalPrice ?? 0);

                        var price = combo.BasePrice + priceSize;
                        var priceEndow = GetPriceEndown(cartItem.ComboId, combo.BasePrice, priceSize);
                        if (priceEndow>0)
                        {
                            tongEndow += (price - priceEndow) * cartItem.Quantity;
                        }
                        
                    }
                }

                if (item.TongTienKhuyenMai != tongEndow)
                    throw new Exception("Khuyến mãi đã có sự thay đổi, vui lòng tải lại trang");
            }

            // Kiểm tra mã giảm giá hết lượt sử dụng
            if (item.DiscountCodeId != Guid.Empty)
            {
                var code = await _unitOfWork.DiscountCodeRepo.GetByIdAsync(item.DiscountCodeId);
                if (code is not null &&
                    code.ModerationStatus == ModerationStatus.Approved &&
                    code.StartDate <= DateTime.Now &&
                    DateTime.Now <= code.EndDate)
                {
                    if (code.UsedCount >= code.UsageLimit)
                        throw new Exception("Mã giảm giá đã hết lượt sử dụng");
                }
            }

            // Nếu không có lỗi → không trả về gì cả
        }


        public async Task<Guid> CheckOutDatHangTaiQuay(CheckOutTaiQuayDto item)
        {

            var cart =await _unitOfWork.CartRepo.GetByIdAsync(item.CartId);
            if (cart is not null)
            {
                try
                {


                    _unitOfWork.BeginTransaction();
                    var cartProductItems = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
                    var cartComboItems = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

                    var ProductIds = cartProductItems.Select(x => x.ProductId).ToList();
                    var ComboIds = cartComboItems.Select(x => x.ComboId).ToList();

                    foreach (var cp in cartProductItems)
                    {
                        var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cp.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                        if (size is not null)
                        {
                            throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                        }
                    }

                    foreach (var cp in cartComboItems)
                    {
                        var comboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cp.Id);
                        foreach (var cpi in comboProductItem)
                        {
                            var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cpi.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                            if (size is not null)
                            {
                                throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                            }
                        }

                    }

                    foreach (var p in ProductIds)
                    {
                        var productCheck = _unitOfWork.ProductRepo.FindWhere(x => x.Id == p && x.ModerationStatus == ModerationStatus.Rejected);
                        if (productCheck.Count() > 0)
                        {
                            throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                        }
                    }

                    foreach (var c in ComboIds)
                    {
                        var comboCheck = _unitOfWork.ComboRepo.FindWhere(x => x.Id == c && x.ModerationStatus == ModerationStatus.Rejected);
                        if (comboCheck.Count() > 0)
                        {
                            throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                        }
                    }

                   

                    if (!cartProductItems.Any() && !cartComboItems.Any())
                    {
                        throw new Exception("Giỏ hàng trống");
                    }

                    bool isCoSPHoatDong = false;
                    foreach (var p in cartProductItems)
                    {
                        var product = await _unitOfWork.ProductRepo.GetByIdAsync(p.ProductId);

                        if (product is not null && product.ModerationStatus == ModerationStatus.Approved)
                        {
                            isCoSPHoatDong = true;
                        }

                    }
                    foreach (var c in cartComboItems)
                    {
                        var combo = await _unitOfWork.ComboRepo.GetByIdAsync(c.ComboId);

                        if (combo is not null && combo.ModerationStatus == ModerationStatus.Approved)
                        {
                            isCoSPHoatDong = true;
                        }
                    }
                    if (!isCoSPHoatDong)
                    {
                        throw new Exception("Giỏ hàng trống");
                    }

                    var store = _unitOfWork.StoresRepo.FirstOrDefault(x => x.Status == Status.Activity);
                    if (store is not null)
                    {
                        var bill = new Bill
                        {
                            UserId = item.NhanVienId,
                            BillCode = BillCodeGen(ReceivingType.PickUpAtStore),
                            StoreId = store.Id,
                            TotalAmount = 0,
                            DiscountAmount = 0,
                            BillDetails = new List<BillDetails>(),
                            Status = StatusOrder.DangChuanBi,
                            ReceivingType = ReceivingType.PickUpAtStore,
                            PhuongThucDatHang = Share.Model.Enum.PhuongThucDatHangEnum.DatTaiQuay


                        };
                        _unitOfWork.BillRepo.Add(bill);
                        await _unitOfWork.CompleteAsync();

                        foreach (var cartItem in cartProductItems)
                        {
                            var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                            if (product == null) continue;
                            if (product.ModerationStatus==ModerationStatus.Approved)
                            {
                                var size = _unitOfWork.SizesRepo.GetById(cartItem.SizeId ?? Guid.Empty);
                                var sizeName = size?.SizeName ?? "Tiêu chuẩn";


                                bill.TotalAmount += product.BasePrice * cartItem.Quantity;

                                var billDetail = new BillDetails
                                {
                                    BillId = bill.Id,
                                    ItemId = product.Id,
                                    ItemType = ItemType.Product,
                                    ItemsName = product.ProductName + " " + "(Size: " + sizeName + ")",
                                    ImageUrl = product.ImageUrl,
                                    Quantity = cartItem.Quantity,
                                    Price = product.BasePrice + (size?.AdditionalPrice ?? 0),
                                    PriceEndow = GetPriceEndown(cartItem.ProductId, product.BasePrice, size?.AdditionalPrice ?? 0),
                                };
                                bill.BillDetails.Add(billDetail);
                                _unitOfWork.BillDetailsRepo.Add(billDetail);
                                await _unitOfWork.CompleteAsync();
                            }
                           
                        }

                        foreach (var cartItem in cartComboItems)
                        {
                            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                            if (combo == null) continue;
                            if (combo.ModerationStatus==ModerationStatus.Approved)
                            {
                                decimal priceSize = 0;
                                var comboItems = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cartItem.Id).ToList();

                                foreach (var c in comboItems)
                                {
                                    var size = await _unitOfWork.SizesRepo.GetByIdAsync(c.SizeId ?? Guid.Empty);
                                    if (size is not null)
                                    {
                                        priceSize += size.AdditionalPrice;
                                    }

                                }

                                bill.TotalAmount += combo.BasePrice * cartItem.Quantity;

                                var billDetail = new BillDetails
                                {
                                    BillId = bill.Id,
                                    ItemId = combo.Id,
                                    ItemType = ItemType.Combo,
                                    ItemsName = combo.ComboName,
                                    ImageUrl = combo.ImageUrl,
                                    Quantity = cartItem.Quantity,
                                    Price = combo.BasePrice + priceSize,
                                    PriceEndow = GetPriceEndown(cartItem.ComboId, combo.BasePrice, priceSize),
                                };
                                bill.BillDetails.Add(billDetail);
                                _unitOfWork.BillDetailsRepo.Add(billDetail);
                                await _unitOfWork.CompleteAsync();

                                foreach (var i in comboItems)
                                {
                                    var product = await _unitOfWork.ProductRepo.GetByIdAsync(i.ProductId);
                                    var sizeName = _unitOfWork.SizesRepo.GetById(i.SizeId ?? Guid.Empty)?.SizeName ?? "Tiêu chuẩn";
                                    if (product is not null)
                                    {
                                        ComboItemsArchive ComboItemsArchive = new ComboItemsArchive()
                                        {
                                            BillDetailsId = billDetail.Id,
                                            ProductId = product.Id,
                                            ProductName = product.ProductName + " " + "(Size: " + sizeName + ")",
                                            Quantity = i.Quantity,
                                            ImageUrl = product.ImageUrl,
                                            Price = product.BasePrice,
                                        };
                                        _unitOfWork.ComboItemsArchiveRepo.Add(ComboItemsArchive);
                                        await _unitOfWork.CompleteAsync();
                                    }



                                }
                            }
                           
                        }



                        
                        BillNotes billNotes1 = new BillNotes()
                        {
                            BillId = bill.Id,
                            NoteType = NoteType.CustomerOrder,
                            NoteContent = "Đơn hàng đã được đặt tại quầy",
                            CreatedBy = Guid.Empty

                        };
                        _unitOfWork.BillNotesRepo.Add(billNotes1);
                        await _unitOfWork.CompleteAsync();

                        if (!string.IsNullOrEmpty(item.GhiChu))
                        {
                            BillNotes billNotes2 = new BillNotes()
                            {
                                BillId = bill.Id,
                                NoteType = NoteType.Internal,
                                NoteContent = "Ghi chú: " + item.GhiChu,
                                CreatedBy = Guid.Empty

                            };

                            _unitOfWork.BillNotesRepo.Add(billNotes2);
                            await _unitOfWork.CompleteAsync();
                        }

                       


                        var billDetails = _unitOfWork.BillDetailsRepo.FindWhere(x => x.BillId == bill.Id).ToList();

                        BillPayment BillPayment = new BillPayment()
                        {
                            BillId = bill.Id,
                            PaymentType = item.PhuongThucThanhToan,
                            Amount = billDetails.Where(x => x.PriceEndow > 0).Sum(p => p.Price * p.Quantity - p.PriceEndow * p.Quantity),
                        };
                        _unitOfWork.BillPaymentRepo.Add(BillPayment);
                        await _unitOfWork.CompleteAsync();

                        var billUpdate = await _unitOfWork.BillRepo.GetByIdAsync(bill.Id);
                        if (billUpdate is not null)
                        {
                            billUpdate.TotalAmount = billDetails.Sum(x => x.Price * x.Quantity);
                            billUpdate.TotalAmountEndow = billDetails.Where(x => x.PriceEndow > 0).Sum(p => p.Price * p.Quantity - p.PriceEndow * p.Quantity);

                            if (item.TongTienKhuyenMai != billUpdate.TotalAmountEndow)
                            {
                                throw new Exception("Khuyến mãi đã có sự thay đổi");
                            }
                            _unitOfWork.BillRepo.Update(billUpdate);
                        }


                        _unitOfWork.CartRepo.Delete(cart);

                        await _unitOfWork.CompleteAsync();
                        await _unitOfWork.CommitAsync();
                        return bill.Id;
                    }
                    else
                    {
                        throw new Exception("Cửa hàng tạm thời đóng cửa");
                    }

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy giỏ hàng");
            }

        }
        public async Task ValidateCheckOutDatHangTaiQuay(CheckOutTaiQuayDto item)
        {
            var cart = await _unitOfWork.CartRepo.GetByIdAsync(item.CartId);
            if (cart == null)
                throw new Exception("Không tìm thấy giỏ hàng");

            var cartProductItems = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
            var cartComboItems = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

            if (!cartProductItems.Any() && !cartComboItems.Any())
                throw new Exception("Giỏ hàng trống");

            foreach (var cp in cartProductItems)
            {
                var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cp.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                if (size is not null)
                {
                    throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                }
            }

            foreach (var cp in cartComboItems)
            {
                var comboProductItem = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cp.Id);
                foreach (var cpi in comboProductItem)
                {
                    var size = _unitOfWork.SizesRepo.FirstOrDefault(x => x.Id == cpi.SizeId && x.ModerationStatus == ModerationStatus.Rejected);
                    if (size is not null)
                    {
                        throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang");
                    }
                }

            }

            // 1. Kiểm tra sản phẩm bị từ chối
            var rejectedProduct = _unitOfWork.ProductRepo.FindWhere(x =>
                cartProductItems.Select(i => i.ProductId).Contains(x.Id)
                && x.ModerationStatus == ModerationStatus.Rejected
            );
            if (rejectedProduct.Any())
                throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang (sản phẩm hết hàng)");

            // 2. Kiểm tra combo bị từ chối
            var rejectedCombo = _unitOfWork.ComboRepo.FindWhere(x =>
                cartComboItems.Select(i => i.ComboId).Contains(x.Id)
                && x.ModerationStatus == ModerationStatus.Rejected
            );
            if (rejectedCombo.Any())
                throw new Exception("Đơn hàng đã có sự thay đổi, vui lòng tải lại trang (combo hết hàng)");

            // 3. Kiểm tra có ít nhất 1 sản phẩm hoặc combo đang hoạt động
            bool isCoSPHoatDong = false;

            foreach (var p in cartProductItems)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(p.ProductId);
                if (product?.ModerationStatus == ModerationStatus.Approved)
                {
                    isCoSPHoatDong = true;
                    break;
                }
            }

            if (!isCoSPHoatDong)
            {
                foreach (var c in cartComboItems)
                {
                    var combo = await _unitOfWork.ComboRepo.GetByIdAsync(c.ComboId);
                    if (combo?.ModerationStatus == ModerationStatus.Approved)
                    {
                        isCoSPHoatDong = true;
                        break;
                    }
                }
            }

            if (!isCoSPHoatDong)
                throw new Exception("Giỏ hàng trống (không có sản phẩm/combo hoạt động)");

            // 4. Kiểm tra tổng tiền khuyến mãi (nếu có)
            if (item.TongTienKhuyenMai > 0)
            {
                decimal tongKhuyenMai = 0;

                foreach (var cartItem in cartProductItems)
                {
                    var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                    var size = _unitOfWork.SizesRepo.GetById(cartItem.SizeId ?? Guid.Empty);
                    if (product?.ModerationStatus == ModerationStatus.Approved)
                    {
                        var basePrice = product.BasePrice + (size?.AdditionalPrice ?? 0);
                        var endowPrice = GetPriceEndown(cartItem.ProductId, product.BasePrice, size?.AdditionalPrice ?? 0);
                        if (endowPrice>0)
                        {
                            tongKhuyenMai += (basePrice - endowPrice) * cartItem.Quantity;
                        }
                       
                    }
                }

                var comboItems = _unitOfWork.ComboProductItemRepository
                    .FindWhere(x => cartComboItems.Select(c => c.Id).Contains(x.CartComboId)).ToList();

                foreach (var cartItem in cartComboItems)
                {
                    var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                    if (combo?.ModerationStatus == ModerationStatus.Approved)
                    {
                        decimal priceSize = 0;
                        var items = comboItems.Where(x => x.CartComboId == cartItem.Id).ToList();
                        foreach (var i in items)
                        {
                            var size = await _unitOfWork.SizesRepo.GetByIdAsync(i.SizeId ?? Guid.Empty);
                            priceSize += size?.AdditionalPrice ?? 0;
                        }

                        var basePrice = combo.BasePrice + priceSize;
                        var endowPrice = GetPriceEndown(combo.Id, combo.BasePrice, priceSize);
                        if (endowPrice>0)
                        {
                            tongKhuyenMai += (basePrice - endowPrice) * cartItem.Quantity;

                        }
                    }
                }

                if (item.TongTienKhuyenMai != tongKhuyenMai)
                    throw new Exception("Khuyến mãi đã có sự thay đổi");
            }

            // 5. Kiểm tra cửa hàng còn hoạt động
            var store = _unitOfWork.StoresRepo.FirstOrDefault(x => x.Status == Status.Activity);
            if (store == null)
                throw new Exception("Cửa hàng tạm thời đóng cửa");

            // Nếu tới đây không lỗi → hợp lệ
        }

        public int GetCartQuantity(Guid userId)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == userId);
            if (cart is not null)
            {
                var cartProductItem = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
                var cartComboItem = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

                int totalQuantity = cartProductItem.Sum(p => p.Quantity) + cartComboItem.Sum(c => c.Quantity);
                return totalQuantity;
            }
            return 0;


        }
        private decimal GetProductPriceEndown(Guid productId, decimal BasePrice, decimal SizePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == productId).ToList();
            List<decimal> PriceEndowns = new List<decimal>();

            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now && x.ModerationStatus == ModerationStatus.Approved);
                foreach (var promotion in promotions)
                {
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        PriceEndowns.Add(promotion.PromotionValue + SizePrice);

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            PriceEndowns.Add(1000 + SizePrice);
                        }
                        else
                        {
                            PriceEndowns.Add(BasePrice - promotion.PromotionValue + SizePrice);
                        }

                    }
                }

            }
            if (PriceEndowns.Count() > 0)
            {
                return PriceEndowns.Min();
            }
            return 0;

        }

        private decimal GetComboPriceEndown(Guid ComboId, decimal BasePrice, decimal SizePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == ComboId).ToList();
            List<decimal> PriceEndowns = new List<decimal>();
            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now && x.ModerationStatus == ModerationStatus.Approved);
                foreach (var promotion in promotions)
                {
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        PriceEndowns.Add(promotion.PromotionValue + SizePrice);

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            PriceEndowns.Add(1000 + SizePrice);
                        }
                        else
                        {
                            PriceEndowns.Add(BasePrice - promotion.PromotionValue + SizePrice);
                        }

                    }
                }
               
            }
            if (PriceEndowns.Count() > 0)
            {
                return PriceEndowns.Min();
            }
            return 0;

        }

        public async Task UpdateQuantity(QuantityInCartDto QuantityInCartDto)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == QuantityInCartDto.UserId);
            if (cart is not null)
            {
                if (QuantityInCartDto.ItemType == ItemType.Product)
                {
                    var CartProductItem = await _unitOfWork.CartItemRepo.GetByIdAsync(QuantityInCartDto.ItemId);
                    if (CartProductItem is not null)
                    {
                        CartProductItem.Quantity = CartProductItem.Quantity + QuantityInCartDto.QuantityThayDoi;
                        _unitOfWork.CartItemRepo.Update(CartProductItem);
                        await _unitOfWork.CompleteAsync();
                        return;
                    }
                }
                else if (QuantityInCartDto.ItemType == ItemType.Combo)
                {
                    var CartProductItem = await _unitOfWork.CartComboItemRepo.GetByIdAsync(QuantityInCartDto.ItemId);
                    if (CartProductItem is not null)
                    {
                        CartProductItem.Quantity = CartProductItem.Quantity + QuantityInCartDto.QuantityThayDoi;
                        _unitOfWork.CartComboItemRepo.Update(CartProductItem);
                        await _unitOfWork.CompleteAsync();
                        return;
                    }
                }
            }
            throw new Exception("Không tìm thấy giỏ hàng với user: " + QuantityInCartDto.UserId);
        }

        public AddressDto GetAddressCheckout(Guid userId)
        {
            var address = _unitOfWork.AddressRepo
                .FirstOrDefault(x => x.UserId == userId && x.AddressType == AddressType.Default);
            if (address is not null)
            {
                AddressDto AddressDto = new AddressDto()
                {
                    Id = address.Id,
                    UserId = address.UserId,
                    FullName = address.FullName,
                    NumberPhone = address.NumberPhone,
                    Province = address.Province,
                    District = address.District,
                    Ward = address.Ward,
                    SpecificAddress = address.SpecificAddress,
                    Latitude = address.Latitude,
                    Longitude = address.Longitude,
                    FullAddress = address.FullAddress,
                    AddressType = address.AddressType
                };
                return AddressDto;
            }
            return new AddressDto();

        }

        private decimal GetPriceEndown(Guid productId, decimal BasePrice, decimal SizePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == productId).ToList();
            List<decimal> PriceEndowns = new List<decimal>();
            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now && x.ModerationStatus == ModerationStatus.Approved);
                foreach (var promotion in promotions)
                {
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        PriceEndowns.Add(promotion.PromotionValue + SizePrice);

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            PriceEndowns.Add(1000 + SizePrice);
                        }
                        else
                        {
                            PriceEndowns.Add(BasePrice - promotion.PromotionValue + SizePrice);
                        }

                    }
                }
                 
            }
            if (PriceEndowns.Count() > 0)
            {
                return PriceEndowns.Min();
            }
            return 0;

        }
        private string BillCodeGen(ReceivingType receivingType)
        {
            DateTime now = DateTime.Now;
            string thoiGian = now.ToString("yyyyMMdd-HHmmss");
            if (receivingType == ReceivingType.HomeDelivery)
            {
                return "GiaoTanNoi" + thoiGian;
            }
            else
            {
                return "NhanTaiQuay" + thoiGian;
            }

        }

        
    }
}