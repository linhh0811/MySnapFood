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
        
    }
}