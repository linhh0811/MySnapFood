using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Infrastructure.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            return await _context
                .Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }
        public async Task<Cart?> GetCartWithItemsAsyncByUserId(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
