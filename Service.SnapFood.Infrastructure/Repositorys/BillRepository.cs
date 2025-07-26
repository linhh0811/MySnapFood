using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Infrastructure.Repositorys.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Repositorys
{
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        private readonly AppDbContext _context;
        public BillRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Bill>> FindAllAsync(Expression<Func<Bill, bool>> predicate)
        {
            return await _context.Bill.Where(predicate).ToListAsync();
        }
    }
}
