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
    public class ComboItemsArchiveRepository : Repository<ComboItemsArchive>, IComboItemsArchiveRepository
    {
        private readonly AppDbContext _context;
        public ComboItemsArchiveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
