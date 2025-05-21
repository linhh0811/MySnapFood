using Service.SnapFood.Domain.Entitys;
using Service.SnapFood.Share.Interface.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Domain.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetCartWithItemsAsync(Guid cartId);
        Task<Cart?> GetCartWithItemsAsyncByUserId(Guid userId);

    }
}
