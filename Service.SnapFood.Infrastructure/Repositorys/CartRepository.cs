using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Infrastructure.Repositorys.Base;

namespace Service.SnapFood.Infrastructure.Repositorys
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartWithItemsAsync(Guid cartId)
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart != null)
            {
                cart.CartProductItems = await _context.CartProductItems
                    .Where(ci => ci.CartId == cartId)
                    .ToListAsync();
                cart.CartComboItems = await _context.CartComboItems
                    .Where(ci => ci.CartId == cartId)
                    .ToListAsync();
                foreach (var comboItem in cart.CartComboItems)
                {
                    comboItem.ComboProductItems = await _context.ComboProductItems
                        .Where(cpi => cpi.CartComboId == comboItem.Id)
                        .ToListAsync();
                }
            }
            return cart;
        }

        public async Task<Cart?> GetCartWithItemsAsyncByUserId(Guid userId)
        {
            // 1. Lấy Cart
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null) return null;

            // 2. Lấy CartProductItems, gom các Id để query batch Products/Size
            var productItems = await _context.CartProductItems
                .Where(ci => ci.CartId == cart.Id)
                .ToListAsync();

            var productIds = productItems
                .Select(ci => ci.ProductId)
                .Distinct()
                .ToList();
            var sizeIds = productItems
                .Select(ci => ci.SizeId)
                .Distinct()
                .ToList();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
            var sizes = await _context.Sizes
                .Where(s => sizeIds.Contains(s.Id))
                .ToListAsync();

            foreach (var ci in productItems)
            {
                ci.Product = products.FirstOrDefault(p => p.Id == ci.ProductId)!;
                ci.Size = sizes.FirstOrDefault(s => s.Id == ci.SizeId)!;
            }
            cart.CartProductItems = productItems;

            // 3. Lấy CartComboItems và ComboProductItems
            var comboItems = await _context.CartComboItems
                .Where(ci => ci.CartId == cart.Id)
                .ToListAsync();

            var comboIds = comboItems
                .Select(ci => ci.ComboId)
                .Distinct()
                .ToList();
            var combos = await _context.Combos
                .Where(co => comboIds.Contains(co.Id))
                .ToListAsync();

            // Lấy tất cả ComboProductItems liên quan
            var comboProductItems = await _context.ComboProductItems
                .Where(cpi => comboIds.Contains(cpi.CartComboId))
                .ToListAsync();

            // Tiếp tục gom Ids để batch load Product và Size cho ComboProductItems
            var comboProdIds = comboProductItems
                .Select(cpi => cpi.ProductId)
                .Distinct()
                .ToList();
            var comboSizeIds = comboProductItems
                .Select(cpi => cpi.SizeId)
                .Distinct()
                .ToList();

            var comboProducts = await _context.Products
                .Where(p => comboProdIds.Contains(p.Id))
                .ToListAsync();
            var comboSizes = await _context.Sizes
                .Where(s => comboSizeIds.Contains(s.Id))
                .ToListAsync();

            // Gán Combo và ComboProductItems vào từng CartComboItem
            foreach (var ci in comboItems)
            {
                ci.Combo = combos.FirstOrDefault(c => c.Id == ci.ComboId)!;
                ci.ComboProductItems = comboProductItems
                    .Where(cpi => cpi.CartComboId == ci.Id)
                    .Select(cpi =>
                    {
                        // với mỗi ComboProductItem, gán Product và Size
                        cpi.Product = comboProducts.First(p => p.Id == cpi.ProductId);
                        cpi.Size = comboSizes.First(s => s.Id == cpi.SizeId);
                        return cpi;
                    })
                    .ToList();
            }
            cart.CartComboItems = comboItems;

            return cart;
        }
    }
}