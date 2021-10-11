using EntityFramework.Entities.Base;
using EntityFramework.Specifications.Interfaces;
using EntityFrameworkExtension.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Specifications;

namespace EntityFramework.Repositories.Interfaces
{
    public interface IAsyncRepository<T> where T : class,IBaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetSingleAsync(ISpecification<T> spec);
        Task<T> GetSingleWithNoTrackAsync(ISpecification<T> spec);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter);
        Task<T> GetSingleWithNoTrackAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAsync(ISpecification<T> spec);
        Task<List<T>> GetAsync<TKey>(ISpecification<T> spec, Expression<Func<T, TKey>> orderBy);
        Task<List<T>> GetWithNoTrackAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetWithNoTrackAsync(ISpecification<T> spec);
        Task<List<T>> GetAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> orderBy);
        Task<List<T>> GetWithoutFilterAsync(ISpecification<T> spec);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> filter);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
