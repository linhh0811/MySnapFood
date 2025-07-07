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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Address>> FindTrackedAsync(Expression<Func<Address, bool>> predicate)
        {
              return await _context.addresses
                         .Where(predicate)
                         .ToListAsync();
        }
    }
}
