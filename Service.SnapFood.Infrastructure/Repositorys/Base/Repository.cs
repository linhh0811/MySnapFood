using Microsoft.EntityFrameworkCore;
using Service.SnapFood.Infrastructure.EF.Contexts;
using Service.SnapFood.Share.Interface.SQL;
using Service.SnapFood.Share.Model.SQL;
using Service.SnapFood.Share.Query.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.SnapFood.Infrastructure.Repositorys.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }



        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T? GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            return entities;
        }

        public IEnumerable<T> FindWhere(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Where(criteria).ToList();
        }

        public IQueryable<T> FilterData(Func<IQueryable<T>, IQueryable<T>> filterFunc, GridRequest gridRequest, ref int totalRecords)
        {
            var query = _context.Set<T>().AsQueryable();
            if (filterFunc != null)
            {
                query = filterFunc(query);
            }

            // Áp dụng bộ lọc và sắp xếp phân trang
            query = GetByGridRequest<T, DefaultClass, DefaultClass, DefaultClass>(query, gridRequest, ref totalRecords);

            return query;
        }
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.
        private IQueryable<T> GetByGridRequest<C, O, N, G>(IQueryable<T> source, GridRequest request, ref int totalRecords)
        {
            Expression methodCallExpression = source.Expression;
            LambdaExpression lambda;
            List<ParameterExpression> lstParameter = new List<ParameterExpression>();
            lstParameter.Add(Expression.Parameter(typeof(C), "objT"));
            lstParameter.Add(Expression.Parameter(typeof(O), "objU"));
            lstParameter.Add(Expression.Parameter(typeof(N), "objV"));
            lstParameter.Add(Expression.Parameter(typeof(G), "objW"));
            List<Type> lstTypeObject = new List<Type>();
            lstTypeObject.Add(typeof(C));
            lstTypeObject.Add(typeof(O));
            lstTypeObject.Add(typeof(N));
            lstTypeObject.Add(typeof(G));

            List<MethodInfo> lstMethodAny = new List<MethodInfo>();
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            #region dùng trong tìm kiếm

            if (request.filter != null && request.filter.Filters != null)
            {
                List<Expression> lstExpression = new List<Expression>();
                foreach (Filter currentfilter in request.filter.Filters)
                {
                    Expression temp = GetExpressionDeQuy<C, O, N, G>(currentfilter, lstParameter, lstTypeObject);
                    if (temp != null)
                        lstExpression.Add(temp);
                }
                if (lstExpression.Count > 0)
                {
                    Expression combinedExpression = lstExpression[0];

                    for (int i = 1; i < lstExpression.Count; i++)
                    {
                        if (request.filter.Logic == "or")
                        {
                            combinedExpression = Expression.OrElse(combinedExpression, lstExpression[i]);
                        }
                        else if (request.filter.Logic == "and")
                        {
                            combinedExpression = Expression.AndAlso(combinedExpression, lstExpression[i]);
                        }
                    }

                    var predicate = Expression.Lambda<Func<T, bool>>(combinedExpression, lstParameter[0]);
                    methodCallExpression = Expression.Call(
                        typeof(Queryable), "Where",
                        new Type[] { source.ElementType },
                        methodCallExpression, Expression.Quote(predicate));
                }
            }
            source = source.Provider.CreateQuery<T>(methodCallExpression);
            #endregion

            #region dùng trong sắp xếp
            if (!(request.sort != null && request.sort.Count > 0))
            {
                List<Sort> lstSort = new List<Sort>() { new Sort()
                {
                    dir = "asc",
                    field = "Id"
                }};
                request.sort = lstSort;
            }
            string propertyName = request.sort[0].field;
            string methodName = (request.sort[0].dir == "asc") ? "OrderBy" : "OrderByDescending";
            string[] tmpArraySortField = propertyName.Split('.');
            ParameterExpression parameter;
            MemberExpression tmpProperty;
            if (tmpArraySortField.Length == 1)
            {
                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, propertyName);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            else
            {
                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, tmpArraySortField[0]);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            for (int iii = 1; iii < request.sort.Count; iii++)
            {
                propertyName = request.sort[iii].field;
                methodName = (request.sort[iii].dir == "asc") ? "ThenBy" : "ThenByDescending";

                parameter = Expression.Parameter(source.ElementType, String.Empty);
                tmpProperty = Expression.Property(parameter, propertyName);
                lambda = Expression.Lambda(tmpProperty, parameter);
                methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, tmpProperty.Type },
                                                source.Expression, Expression.Quote(lambda));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            #endregion


            #region dùng trong phân trang

            totalRecords = source.Count();

            if (request.page > 0 && request.pageSize > 0)
            {

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Skip",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant((request.page - 1) * request.pageSize));
                source = source.Provider.CreateQuery<T>(methodCallExpression);

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Take",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant(request.pageSize));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }

            #endregion


            return source;
        }
        private Expression GetExpressionDeQuy<C, O, N, G>(Filter currentfilter, List<ParameterExpression> lstParameter, List<Type> lstTypeObject)
        {
            Expression? result = null;
            if (string.IsNullOrEmpty(currentfilter.Logic))
            {
                #region So sánh trực tiếp
                List<string>? lstField = currentfilter?.Field?.Split('.').ToList();
                string? strValue = currentfilter?.Value?.Trim();
                string? strPhuongThuc = currentfilter?.Method;
                if (lstField?.Count == 1)
                {
                    MemberExpression express = Expression.Property(lstParameter[0], currentfilter?.Field);
                    Type typeField = express.Type;
                    Expression temp = GetExpresionByType(typeField, express, strValue, strPhuongThuc);
                    if (temp != null)
                        result = temp;
                }
                else if (lstField?.Count > 1)
                {
                    #region Trường hợp any
                    ParameterExpression parameterFieldCompare = lstParameter[lstField.Count - 1];
                    MemberExpression express = Expression.Property(parameterFieldCompare, lstField[lstField.Count - 1]);
                    Type typeField = express.Type;
                    Expression temp = GetExpresionByType(typeField, express, strValue, strPhuongThuc);
                    result = AddQueryAny<C, O, N, G>(temp, lstParameter, lstTypeObject, lstField);
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Điều kiện lồng
                List<Expression> lstExpression = new List<Expression>();
                foreach (Filter childFilter in currentfilter.Filters)
                {
                    Expression temp = GetExpressionDeQuy<C, O, N, G>(childFilter, lstParameter, lstTypeObject);
                    if (temp != null)
                        lstExpression.Add(temp);
                }

                if (lstExpression.Count > 0)
                {
                    BinaryExpression epSum;
                    if (currentfilter.Logic == "or")
                    {
                        if (lstExpression.Count == 1)
                            epSum = Expression.OrElse(lstExpression[0], lstExpression[0]);
                        else
                        {
                            int countExpression = 2;
                            epSum = Expression.OrElse(lstExpression[0], lstExpression[1]);
                            while (countExpression < lstExpression.Count)
                            {
                                epSum = Expression.OrElse(epSum, lstExpression[countExpression]);
                                countExpression++;
                            }
                        }
                        result = epSum;
                    }
                    else
                    {
                        if (lstExpression.Count == 1)
                            epSum = Expression.And(lstExpression[0], lstExpression[0]);
                        else
                        {
                            int countExpression = 2;
                            epSum = Expression.And(lstExpression[0], lstExpression[1]);
                            while (countExpression < lstExpression.Count)
                            {
                                epSum = Expression.And(epSum, lstExpression[countExpression]);
                                countExpression++;
                            }
                        }
                        result = epSum;
                    }
                }
                #endregion
            }
            return result;
        }

        private Expression GetExpresionByType(Type typeField, MemberExpression express, string strValue, string strPhuongThuc)
        {
            if (typeField == typeof(Int64)
                                || typeField == typeof(Int32)
                                || typeField == typeof(Int64?)
                                || typeField == typeof(Int32?)
                                || typeField == typeof(double)
                                || typeField == typeof(double?)
                                || typeField == typeof(decimal)
                                || typeField == typeof(decimal?))
            {
                return AddQueryNumeric(express, strValue, strPhuongThuc);
            }
            else if (typeField == typeof(DateTime) || typeField == typeof(DateTime?))
            {
                return AddQueryDateTime(express, strValue, strPhuongThuc);
            }
            else if (typeField == typeof(string))
            {
                return AddQueryString(express, strValue, strPhuongThuc);
            }
            return null;
        }
        private Expression? AddQueryAny<C, O, N, G>(Expression expresionLast, List<ParameterExpression> lstParameter, List<Type> lstTypeObject, List<string> lstField)
        {
            MethodCallExpression? result = null;
            for (int i = lstField.Count - 2; i >= 0; i--)
            {
                ParameterExpression parameterFieldCompare = lstParameter[i];
                MemberExpression express = Expression.Property(parameterFieldCompare, lstField[i]);
                MethodInfo method = typeof(Enumerable).
                                    GetMethods().
                                    Where(x => x.Name == "Any").
                                    Single(x => x.GetParameters().Length == 2).
                                    MakeGenericMethod(lstTypeObject[lstField.Count - i - 1]);
                LambdaExpression? lambda = null;

                if (result == null)
                {
                    if (i == 0)
                        lambda = Expression.Lambda<Func<O, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                    else if (i == 1)
                        lambda = Expression.Lambda<Func<N, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                    if (i == 2)
                        lambda = Expression.Lambda<Func<G, bool>>(expresionLast, lstParameter[lstField.Count - i - 1]);
                }
                else
                {
                    if (i == 0)
                        lambda = Expression.Lambda<Func<O, bool>>(result, lstParameter[lstField.Count - i - 1]);
                    else if (i == 1)
                        lambda = Expression.Lambda<Func<N, bool>>(result, lstParameter[lstField.Count - i - 1]);
                    if (i == 2)
                        lambda = Expression.Lambda<Func<G, bool>>(result, lstParameter[lstField.Count - i - 1]);
                }
                result = Expression.Call(method, express, lambda);
            }
            return result;
        }
        private Expression AddQueryNumeric(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            ConstantExpression Int_values;
            Type typeField = propertyField.Type;
            if (typeField == typeof(double) || typeField == typeof(double?))
                Int_values = Expression.Constant(Convert.ToDouble(strValue));
            else if (typeField == typeof(decimal) || typeField == typeof(decimal?))
                Int_values = Expression.Constant(Convert.ToDecimal(strValue));
            else if (typeField == typeof(Int64))
                Int_values = Expression.Constant(Convert.ToInt64(strValue)); // = "value" + Kiểu giá trị
            else
                Int_values = Expression.Constant(Convert.ToInt32(strValue)); // = "value" + Kiểu giá trị
            var Int_eq = Expression.Equal(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_neq = Expression.NotEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_gte = Expression.GreaterThanOrEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_gt = Expression.GreaterThan(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_lte = Expression.LessThanOrEqual(propertyField, Expression.Convert(Int_values, propertyField.Type));
            var Int_lt = Expression.LessThan(propertyField, Expression.Convert(Int_values, propertyField.Type));
            if (strPhuongThuc == "eq")
                return Int_eq;
            else if (strPhuongThuc == "neq")
                return Int_neq;
            else if (strPhuongThuc == "gte")
                return Int_gte;
            else if (strPhuongThuc == "gt")
                return Int_gt;
            else if (strPhuongThuc == "lte")
                return Int_lte;
            else
                return Int_lt;
        }
        private Expression AddQueryDateTime(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            dtfi.DateSeparator = "/";
            Type typeField = propertyField.Type;
            DateTime? Date_value_date = Convert.ToDateTime(strValue, dtfi);
            ConstantExpression Date_values;
            if (typeField == typeof(DateTime?))
                Date_values = Expression.Constant(Date_value_date, typeof(DateTime?));
            else
                Date_values = Expression.Constant(Date_value_date, typeof(DateTime));

            var Date_eq = Expression.Equal(propertyField, Date_values);
            var Date_neq = Expression.NotEqual(propertyField, Date_values);
            var Date_gte = Expression.GreaterThanOrEqual(propertyField, Date_values);
            var Date_gt = Expression.GreaterThan(propertyField, Date_values);
            var Date_lte = Expression.LessThanOrEqual(propertyField, Date_values);
            var Date_lt = Expression.LessThan(propertyField, Date_values);
            if (strPhuongThuc == "eq")
                return Date_eq;
            else if (strPhuongThuc == "neq")
                return Date_neq;
            else if (strPhuongThuc == "gte")
                return Date_gte;
            else if (strPhuongThuc == "gt")
                return Date_gt;
            else if (strPhuongThuc == "lte")
                return Date_lte;
            else
                return Date_lt;
        }
        private Expression AddQueryString(MemberExpression propertyField, string strValue, string strPhuongThuc)
        {
            var termConstant = Expression.Constant(strValue, typeof(string)); // = "value"
            var ToLower = Expression.Call(propertyField, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            var StartWith = Expression.Call(ToLower, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().StartsWith();
            var Contains = Expression.Call(ToLower, typeof(string).GetMethod("Contains", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().Contains();
            var Equals = Expression.Call(ToLower, typeof(string).GetMethod("Equals", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().Equals();
            var EndsWith = Expression.Call(ToLower, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), termConstant); // obj => obj.ToLower().EndWith();

            if (strPhuongThuc == "contains")
                return Contains;
            else if (strPhuongThuc == "startswith")
                return StartWith;
            else if (strPhuongThuc == "endswith")
                return EndsWith;
            else if (strPhuongThuc == "lt" || strPhuongThuc == "gt")
                return null;
            else
                return Equals;
        }
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8604 // Possible null reference argument.

    }
}