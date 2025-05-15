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
    public class ComboRepository : Repository<Combo>, IComboRepository
    {
        private readonly AppDbContext _context;
        public ComboRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
