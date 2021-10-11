using BusinessLogicShared.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories.Interfaces
{
    public interface IGenericRepositor<TEntity,TModel>
        where TEntity : class
        where TModel : class
    {
        IEnumerable<TEntity> GetAll(
            PagingParameters pagingParameters,
            SortingParameters orderBy = null,
            params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllAsync(
            PagingParameters pagingParameters,
            SortingParameters orderBy = null,
            params string[] includeProperties);

        TEntity GetOne(
            Expression<Func<TModel, bool>> filter = null, params string[] includeProperties);

        Task<TEntity> GetOneAsync(
            Expression<Func<TModel, bool>> filter = null, params string[] includeProperties);

        IEnumerable<TEntity> Get(
            PagingParameters pagingParameters,
            Expression<Func<TModel, bool>> filter = null,
            SortingParameters orderBy = null,
            params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetAsync(
            PagingParameters pagingParameters,
            Expression<Func<TModel, bool>> filter = null,
            SortingParameters orderBy = null,
            params string[] includeProperties);

        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);

        int GetCount(Expression<Func<TModel, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TModel, bool>> filter = null);

        bool GetIsExists(Expression<Func<TModel, bool>> filter = null);

        Task<bool> GetIsExistsAsync(Expression<Func<TModel, bool>> filter = null);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        void Save();

        Task SaveAsync();
    }
}
