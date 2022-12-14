using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.DataAccess.Paging;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess
{
    public interface IAsyncEntityRepository<T>
        where T : class, IEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> expression = null);
        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 10, bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int index = 0, int size = 10, bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
