using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess
{
    public interface IEntityRepository<T>
        where T : class, IEntity
    {
        T Get(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetList(Expression<Func<T, bool>> expression = null);
        IPaginate<T> GetListForPaging(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 10,
            bool enableTracking = true);

        IPaginate<T> GetListForPagingByDynamic(Dynamic.Dynamic dynamic,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 10, bool enableTracking = true);

        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}