using Core.Entities;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.DataAccess.Dynamic;
using Core.DataAccess.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Nest;

namespace Core.DataAccess.EntityFramework
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class EfEntityRepositoryBase<TEntity, TContext>
        : IEntityRepository<TEntity>,IAsyncEntityRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected TContext Context { get; }
        public EfEntityRepositoryBase(TContext context)
        {
            Context = context;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await Context.Set<TEntity>().ToListAsync()
                : await Context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy =
                                                               null,
                                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                               include = null,
                                                           int index = 0, int size = 10, bool enableTracking = true,
                                                           CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
                                                                    Func<IQueryable<TEntity>,
                                                                            IIncludableQueryable<TEntity, object>>?
                                                                        include = null,
                                                                    int index = 0, int size = 10,
                                                                    bool enableTracking = true,
                                                                    CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entity;
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().AsNoTracking()
                : Context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public IPaginate<TEntity> GetListForPaging(Expression<Func<TEntity, bool>>? predicate = null,
                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
                                          int index = 0, int size = 10,
                                          bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public IPaginate<TEntity> GetListForPagingByDynamic(Dynamic.Dynamic dynamic,
                                                   Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                       include = null, int index = 0, int size = 10,
                                                   bool enableTracking = true)
        {
            IQueryable<TEntity> queryable = Query().AsQueryable().ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return queryable.ToPaginate(index, size);
        }

        public TEntity Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return entity;
        }
    }
}