using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFramework.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity
    {
        T GetById(int id);
        T GetSingle(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
