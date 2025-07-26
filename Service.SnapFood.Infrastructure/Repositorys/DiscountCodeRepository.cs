using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Domain.Interfaces;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Infrastructure.Repositorys.Base;
using Service.SnapFood.Share.Interface.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Repositorys
{
    public class DiscountCodeRepository : Repository<DiscountCode>, IDiscountCodeRepository
    {
        private readonly AppDbContext _context;
        public DiscountCodeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
