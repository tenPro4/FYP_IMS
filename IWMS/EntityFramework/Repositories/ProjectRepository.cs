using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicShared.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;

namespace EntityFramework.Repositories
{
    public class ProjectRepository :IProjectRepository
    {
        private readonly IGenericRepositor<MasterProject, MasterProject> _repository;

        public ProjectRepository(IGenericRepositor<MasterProject, MasterProject> repository)
        {
            _repository = repository;
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public virtual void Delete(List<int> ids)
        {
            ids.ToList().ForEach(p => _repository.Delete(p));
            _repository.Save();
        }

        public virtual void Delete(MasterProject model)
        {
            _repository.Delete(model);
            _repository.Save();
        }

        public virtual void Delete(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Delete(p));
            _repository.Save();
        }

        public virtual async Task DeleteAsync(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(List<int> ids)
        {
            ids.ToList().ForEach(p => _repository.Delete(p));
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(MasterProject model)
        {
            _repository.Delete(model);
            await _repository.SaveAsync();
        }

        public virtual async Task DeleteAsync(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Delete(p));
            await _repository.SaveAsync();
        }

        public virtual IEnumerable<MasterProject> Get(Expression<Func<MasterProject, bool>> filter = null, SortingParameters orderBy = null, params string[] includeProperties)
        {
            return _repository.Get(null, filter, orderBy, includeProperties);
        }

        public virtual IEnumerable<MasterProject> GetAll(SortingParameters orderBy = null, params string[] includeProperties)
        {
            return _repository.GetAll(null, orderBy, includeProperties);
        }

        public virtual async Task<IEnumerable<MasterProject>> GetAllAsync(SortingParameters orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAllAsync(null, orderBy, includeProperties);
        }

        public virtual async Task<IEnumerable<MasterProject>> GetAsync(Expression<Func<MasterProject, bool>> filter = null, SortingParameters orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAsync(null, filter, orderBy, includeProperties);
        }

        public virtual MasterProject GetById(int id)
        {
            return  _repository.GetById(id);
        }

        public virtual async Task<MasterProject> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual int GetCount(Expression<Func<MasterProject, bool>> filter = null)
        {
            return _repository.GetCount(filter);
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<MasterProject, bool>> filter = null)
        {
            return await _repository.GetCountAsync(filter);
        }

        public virtual bool GetIsExists(Expression<Func<MasterProject, bool>> filter = null)
        {
            return _repository.GetIsExists(filter);
        }

        public virtual async Task<bool> GetIsExistsAsync(Expression<Func<MasterProject, bool>> filter = null)
        {
            return await _repository.GetIsExistsAsync(filter);
        }

        public virtual MasterProject GetOne(Expression<Func<MasterProject, bool>> filter = null, params string[] includeProperties)
        {
            return _repository.GetOne(filter, includeProperties);
        }

        public virtual async Task<MasterProject> GetOneAsync(Expression<Func<MasterProject, bool>> filter = null, params string[] includeProperties)
        {
            return  await _repository.GetOneAsync(filter, includeProperties);
        }

        public virtual void Insert(MasterProject model)
        {
            _repository.Insert(model);
            _repository.Save();
        }

        public virtual void Insert(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Insert(p));
            _repository.Save();
        }

        public virtual async Task InsertAsync(MasterProject model)
        {
            _repository.Insert(model);
            await _repository.SaveAsync();
        }

        public virtual async Task InsertAsync(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Insert(p));
            await _repository.SaveAsync();
        }

        public virtual void Save()
        {
            _repository.Save();
        }

        public virtual async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

        public virtual void Update(MasterProject model)
        {
            _repository.Update(model);
            _repository.Save();
        }

        public virtual void Update(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Update(p));
            _repository.Save();
        }

        public virtual async Task UpdateAsync(MasterProject model)
        {
            _repository.Update(model);
            await _repository.SaveAsync();
        }

        public virtual async Task UpdateAsync(List<MasterProject> models)
        {
            models.ForEach(p => _repository.Update(p));
            await _repository.SaveAsync();
        }
    }
}
