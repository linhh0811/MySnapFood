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

        public async Task AddComboToCartAsync(AddComboToCartDto item)
        {
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
            if (cart is not null)
            {
                var combo = await _unitOfWork.ComboRepo.GetByIdAsync(item.ComboId);

                if (combo == null)
                    throw new Exception("Combo không còn tồn tại trên hệ thống");

                if (combo.ModerationStatus != ModerationStatus.Approved)
                    throw new Exception("Combo chưa được duyệt");

                var comboProducts = _unitOfWork.ProductComboRepo.FindWhere(pc => pc.ComboId == item.ComboId).ToList();
                if (!comboProducts.Any())
                    throw new Exception("Combo không có sản phẩm nào");

                var cartComboItemIsExist = _unitOfWork.CartComboItemRepo.FirstOrDefault(x => x.ComboId == item.ComboId && x.CartId == cart.Id);



                if (cartComboItemIsExist is null)
                {
                    CartComboItem cartComboItem = new CartComboItem()
                    {
                        CartId = cart.Id,
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
                            CartId = cart.Id,
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
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
            if (cart is not null)
            {
                if (item.SizeId == Guid.Empty || item.SizeId == null)
                {
                    var cartProductItemIsExist = _unitOfWork.CartItemRepo.FirstOrDefault(x => x.ProductId == item.ProductId && x.CartId == cart.Id);
                    if (cartProductItemIsExist is not null)
                    {
                        cartProductItemIsExist.Quantity = cartProductItemIsExist.Quantity + item.Quantity;
                        _unitOfWork.CartItemRepo.Update(cartProductItemIsExist);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        CartProductItem cartProductItem = new CartProductItem();
                        cartProductItem.CartId = cart.Id;
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
                    var cartProductItemIsExist = _unitOfWork.CartItemRepo.FirstOrDefault(x => x.ProductId == item.ProductId && x.SizeId == item.SizeId);
                    if (cartProductItemIsExist is not null)
                    {
                        cartProductItemIsExist.Quantity = cartProductItemIsExist.Quantity + item.Quantity;
                        _unitOfWork.CartItemRepo.Update(cartProductItemIsExist);
                        await _unitOfWork.CompleteAsync();
                    }
                    else
                    {
                        CartProductItem cartProductItem = new CartProductItem();
                        cartProductItem.CartId = cart.Id;
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
                        PriceEndown = GetProductPriceEndown(item.ProductId, product.BasePrice) + (size?.AdditionalPrice ?? 0),
                        Quantity = item.Quantity,
                        ImageUrl = product.ImageUrl,
                        ItemType = ItemType.Product,
                        Created = item.Created,

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
                        PriceEndown = GetComboPriceEndown(item.ComboId, combo.BasePrice) + priceSize,
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
            var cart = _unitOfWork.CartRepo.FirstOrDefault(x => x.UserId == item.UserId);
            if (cart is not null)
            {
                try
                {


                    _unitOfWork.BeginTransaction();
                    var cartProductItems = _unitOfWork.CartItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();
                    var cartComboItems = _unitOfWork.CartComboItemRepo.FindWhere(x => x.CartId == cart.Id).ToList();

                    if (!cartProductItems.Any() && !cartComboItems.Any())
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
                            BillDetails = new List<BillDetails>(),
                            Status = StatusOrder.Pending,

                        };
                        _unitOfWork.BillRepo.Add(bill);
                        await _unitOfWork.CompleteAsync();

                        foreach (var cartItem in cartProductItems)
                        {
                            var product = await _unitOfWork.ProductRepo.GetByIdAsync(cartItem.ProductId);
                            var sizeName = _unitOfWork.SizesRepo.GetById(cartItem.SizeId ?? Guid.Empty)?.SizeName ?? "Tiêu chuẩn";
                            if (product == null) continue;

                            bill.TotalAmount += product.BasePrice * cartItem.Quantity;

                            var billDetail = new BillDetails
                            {
                                BillId = bill.Id,
                                ItemType = ItemType.Product,
                                ItemsName = product.ProductName + " " + (sizeName),
                                ImageUrl = product.ImageUrl,
                                Quantity = cartItem.Quantity,
                                Price = product.BasePrice,
                                PriceEndow = GetPriceEndown(cartItem.ProductId, product.BasePrice),
                            };
                            bill.BillDetails.Add(billDetail);
                            _unitOfWork.BillDetailsRepo.Add(billDetail);
                            await _unitOfWork.CompleteAsync();
                        }

                        foreach (var cartItem in cartComboItems)
                        {
                            var combo = await _unitOfWork.ComboRepo.GetByIdAsync(cartItem.ComboId);
                            if (combo == null) continue;

                            bill.TotalAmount += combo.BasePrice * cartItem.Quantity;

                            var billDetail = new BillDetails
                            {
                                BillId = bill.Id,
                                ItemType = ItemType.Combo,
                                ItemsName = combo.ComboName,
                                ImageUrl = combo.ImageUrl,
                                Quantity = cartItem.Quantity,
                                Price = combo.BasePrice,
                                PriceEndow = GetPriceEndown(cartItem.ComboId, combo.BasePrice),
                            };
                            bill.BillDetails.Add(billDetail);
                            _unitOfWork.BillDetailsRepo.Add(billDetail);
                            await _unitOfWork.CompleteAsync();
                            var comboItems = _unitOfWork.ComboProductItemRepository.FindWhere(x => x.CartComboId == cartItem.Id).ToList();
                            foreach (var i in comboItems)
                            {
                                var product = await _unitOfWork.ProductRepo.GetByIdAsync(i.ProductId);
                                var sizeName = _unitOfWork.SizesRepo.GetById(i.SizeId ?? Guid.Empty)?.SizeName ?? "Tiêu chuẩn";
                                if (product is not null)
                                {
                                    ComboItemsArchive ComboItemsArchive = new ComboItemsArchive()
                                    {
                                        BillDetailsId = billDetail.Id,
                                        ProductName = product.ProductName + " " + sizeName,
                                        Quantity = i.Quantity,
                                        ImageUrl = product.ImageUrl,
                                        Price = product.BasePrice,
                                    };
                                    _unitOfWork.ComboItemsArchiveRepo.Add(ComboItemsArchive);
                                    await _unitOfWork.CompleteAsync();
                                }



                            }
                        }
                        BillPayment BillPayment = new BillPayment()
                        {
                            BillId = bill.Id,
                            PaymentType = item.PaymentType,
                            Amount = 0,
                        };
                        _unitOfWork.BillPaymentRepo.Add(BillPayment);
                        await _unitOfWork.CompleteAsync();
                        if (!string.IsNullOrEmpty(item.Description))
                        {
                            BillNotes billNotes = new BillNotes()
                            {
                                BillId = bill.Id,
                                NoteType = NoteType.CustomerOrder,
                                NoteContent = item.Description,
                                CreatedBy = item.UserId,
                            };
                            _unitOfWork.BillNotesRepo.Add(billNotes);
                            await _unitOfWork.CompleteAsync();
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
                                    Distance = 0,
                                    DeliveryFee = 0,
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
                                ReceiverAddress = "Nhận hàng tại quầy",
                                Distance = 0,
                                DeliveryFee = 0,
                            };
                            _unitOfWork.BillDeliveryRepo.Add(billDelivery);
                            await _unitOfWork.CompleteAsync();
                        }

                        var billDetails = _unitOfWork.BillDetailsRepo.FindWhere(x => x.BillId == bill.Id).ToList();

                        var billUpdate = await _unitOfWork.BillRepo.GetByIdAsync(bill.Id);
                        if (billUpdate is not null)
                        {
                            billUpdate.TotalAmount = billDetails.Sum(x => x.Price * x.Quantity);
                            billUpdate.TotalAmountEndow = billDetails.Where(x => x.PriceEndow > 0).Sum(p => p.Price * p.Quantity - p.PriceEndow * p.Quantity);

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


        //public async Task<int> GetCartQuantityAsync(Guid userId)
        //{
        //    var cart = await _unitOfWork.CartRepo.GetCartWithItemsAsyncByUserId(userId);
        //    if (cart == null)
        //    {
        //        return 0; // Giỏ hàng trống hoặc không tồn tại
        //    }

        //    int totalQuantity = cart.CartProductItems.Sum(p => p.Quantity) + cart.CartComboItems.Sum(c => c.Quantity);
        //    return totalQuantity;
        //}
        private decimal GetProductPriceEndown(Guid productId, decimal BasePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == productId).ToList();
            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now);
                if (promotions.Count() > 0)
                {
                    var promotion = promotions.First();
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        return promotion.PromotionValue;

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            return 1000;
                        }
                        else
                        {
                            return BasePrice - promotion.PromotionValue;
                        }

                    }
                }
            }
            return 0;

        }

        private decimal GetComboPriceEndown(Guid ComboId, decimal BasePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == ComboId).ToList();
            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now);
                if (promotions.Count() > 0)
                {
                    var promotion = promotions.First();
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        return promotion.PromotionValue;

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            return 1000;
                        }
                        else
                        {
                            return BasePrice - promotion.PromotionValue;
                        }

                    }
                }
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

        private decimal GetPriceEndown(Guid productId, decimal BasePrice)
        {
            var promotionItems = _unitOfWork.PromotionItemsRepository.FindWhere(x => x.ItemId == productId).ToList();
            foreach (var item in promotionItems)
            {
                var promotions = _unitOfWork.PromotionRepository.FindWhere(x => x.Id == item.PromotionId && x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now);
                if (promotions.Count() > 0)
                {
                    var promotion = promotions.First();
                    if (promotion.PromotionType == PromotionType.FixedPrice)
                    {
                        return promotion.PromotionValue;

                    }
                    else if (promotion.PromotionType == PromotionType.Amount)
                    {
                        if ((BasePrice - promotion.PromotionValue) <= 0)
                        {
                            return 1000;
                        }
                        else
                        {
                            return BasePrice - promotion.PromotionValue;
                        }

                    }
                }
            }
            return 0;

        }
        private string BillCodeGen(ReceivingType receivingType)
        {
            DateTime now = DateTime.Now;
            string thoiGian = now.ToString("yyyyMMdd-HHmmss");
            if (receivingType== ReceivingType.HomeDelivery)
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