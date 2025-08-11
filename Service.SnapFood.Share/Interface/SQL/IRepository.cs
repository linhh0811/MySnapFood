using Service.SnapFood.Share.Query.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Share.Interface.SQL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);

        void Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);


        void Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        void SaveChanges();
        Task SaveChangesAsync();

        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindWhere(Expression<Func<T, bool>> criteria);

        IQueryable<T> FilterData(Func<IQueryable<T>, IQueryable<T>> filterFunc, GridRequest gridRequest, ref int totalRecords);
        IQueryable<T> Query();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
    }
}
