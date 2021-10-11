using EntityFramework.Context;
using EntityFramework.Entities.Base;
using EntityFramework.Repositories.Interfaces;
using EntityFrameworkExtension.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Specifications.Interfaces;

namespace EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T>, IAsyncRepository<T> 
        where T : class,IBaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public T GetSingle(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).FirstOrDefault();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleWithNoTrackAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbContext.Set<T>().Where(filter).ToList();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetWithNoTrackAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }


        public async Task<List<T>> GetWithNoTrackAsync(ISpecification<T> spec)
        {
            var queryWithIncludes = spec.Includes
               .Aggregate(_dbContext.Set<T>().AsQueryable(),
               (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.Where(spec.Filter)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<T>> GetAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy)
        {
            return await _dbContext.Set<T>().Where(filter)
                .OrderBy(orderBy)
                .ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entity)
        {
            _dbContext.Set<T>().AddRange(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync() > 1;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().AnyAsync(filter);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbContext.Set<T>().CountAsync(filter);
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec)
        {
            var queryWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.Where(spec.Filter)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetSingleWithNoTrackAsync(ISpecification<T> spec)
        {
            var queryWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.Where(spec.Filter)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAsync(ISpecification<T> spec)
        {
            var queryWithIncludes = spec.Includes
               .Aggregate(_dbContext.Set<T>().AsQueryable(),
               (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.Where(spec.Filter)
                .ToListAsync();
        }

        public async Task<List<T>> GetAsync<TKey>(ISpecification<T> spec, Expression<Func<T, TKey>> orderBy)
        {
            var queryWithIncludes = spec.Includes
               .Aggregate(_dbContext.Set<T>().AsQueryable(),
               (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.Where(spec.Filter)
                .OrderBy(orderBy)
                .ToListAsync();
        }

        public async Task<List<T>> GetWithoutFilterAsync(ISpecification<T> spec)
        {
            var queryWithIncludes = spec.Includes
               .Aggregate(_dbContext.Set<T>().AsQueryable(),
               (current, include) => current.Include(include));

            var queryResult = spec.IncludeStrings
                .Aggregate(queryWithIncludes,
                (current, include) => current.Include(include));

            return await queryResult.ToListAsync();
        }
    }
}
