using EntityFramework.Context;
using EntityFramework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using BusinessLogicShared.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace EntityFramework.Repositories
{
    public class GenericRepository<TEntity,TModel> : IGenericRepositor<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        protected readonly AppDbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }

        protected virtual IQueryable<TEntity> GetQueryable(
           PagingParameters pagingParameters,
           Expression<Func<TModel, bool>> filter = null,
           SortingParameters orderBy = null,
           params string[] includeProperties)
        {
            // AsNoTracking - disable loading of all navigation properties automatically. 
            IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();

            // Filtering using Dynamic Linq.
            if (filter != null)
            {
                //var e = DynamicExpressionParser.ParseLambda<TEntity, bool>(new ParsingConfig(), true, LambdaToString.ToString(filter));
                query = query.Where(filter);
            }
                
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }

            if (orderBy != null && orderBy.Sorters.Count > 0)
            {
                foreach (var item in orderBy.Sorters)
                {
                    if (item.IsAscending)
                        query = query.OrderBy(item.OrderBy);
                    else
                        query = query.OrderBy(item.OrderBy + " descending");
                }
            }

            if (pagingParameters != null)
            {
                query = query.Skip(pagingParameters.Skip);
                query = query.Take(pagingParameters.PageSize);
            }

            return query;
        }

        public virtual IEnumerable<TEntity> GetAll(
           PagingParameters pagingParameters,
           SortingParameters orderBy = null,
           params string[] includeProperties)
        {
            return GetQueryable(pagingParameters, null, orderBy, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
           PagingParameters pagingParameters,
           SortingParameters orderBy = null,
           params string[] includeProperties)
        {
            return await GetQueryable(pagingParameters, null, orderBy, includeProperties).ToListAsync();
        }

        public virtual async Task<TEntity> GetOneAsync(Expression<Func<TModel, bool>> filter = null, params string[] includeProperties)
        {
            return await GetQueryable(null, filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual TEntity GetOne(Expression<Func<TModel, bool>> filter = null, params string[] includeProperties)
        {
            return GetQueryable(null, filter, null, includeProperties).SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> Get(
           PagingParameters pagingParameters,
           Expression<Func<TModel, bool>> filter = null,
           SortingParameters orderBy = null,
           params string[] includeProperties)
        {
            return GetQueryable(pagingParameters, filter, orderBy, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            PagingParameters pagingParameters,
            Expression<Func<TModel, bool>> filter = null,
            SortingParameters orderBy = null,
            params string[] includeProperties)
        {
            return await GetQueryable(pagingParameters, filter, orderBy, includeProperties).ToListAsync();
        }

        public virtual TEntity GetById(object id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount(Expression<Func<TModel, bool>> filter = null)
        {
            return GetQueryable(null, filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TModel, bool>> filter = null)
        {
            return GetQueryable(null, filter).CountAsync();
        }

        public virtual bool GetIsExists(Expression<Func<TModel, bool>> filter = null)
        {
            return GetQueryable(null, filter).Any();
        }

        public virtual Task<bool> GetIsExistsAsync(Expression<Func<TModel, bool>> filter = null)
        {
            return GetQueryable(null, filter).AnyAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual Task SaveAsync()
        {
            return context.SaveChangesAsync();

        }
    }
}
