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
    public interface IAddressRepository : IRepository<Address>
    {
        Task<List<Address>> FindTrackedAsync(Expression<Func<Address, bool>> predicate);
    }
}
