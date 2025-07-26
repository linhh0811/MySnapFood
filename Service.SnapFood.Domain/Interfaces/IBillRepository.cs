using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Interface.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<List<Bill>> FindAllAsync(Expression<Func<Bill, bool>> predicate);

    }
}
