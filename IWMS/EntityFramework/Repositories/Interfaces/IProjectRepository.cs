using BusinessLogicShared.Common;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<MasterProject> GetAll(SortingParameters orderBy = null, params string[] includeProperties);
        Task<IEnumerable<MasterProject>> GetAllAsync(SortingParameters orderBy = null, params string[] includeProperties);
        Task<MasterProject> GetOneAsync(Expression<Func<MasterProject, bool>> filter = null, params string[] includeProperties);
        MasterProject GetOne(Expression<Func<MasterProject, bool>> filter = null, params string[] includeProperties);
        IEnumerable<MasterProject> Get(Expression<Func<MasterProject, bool>> filter = null, SortingParameters orderBy = null, params string[] includeProperties);
        Task<IEnumerable<MasterProject>> GetAsync(Expression<Func<MasterProject, bool>> filter = null, SortingParameters orderBy = null, params string[] includeProperties);
        MasterProject GetById(int id);
        Task<MasterProject> GetByIdAsync(int id);
        int GetCount(Expression<Func<MasterProject, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<MasterProject, bool>> filter = null);
        bool GetIsExists(Expression<Func<MasterProject, bool>> filter = null);
        Task<bool> GetIsExistsAsync(Expression<Func<MasterProject, bool>> filter = null);
        void Insert(MasterProject model);
        void Insert(List<MasterProject> models);
        Task InsertAsync(MasterProject model);
        Task InsertAsync(List<MasterProject> models);
        void Update(MasterProject model);
        void Update(List<MasterProject> models);
        Task UpdateAsync(MasterProject model);
        Task UpdateAsync(List<MasterProject> models);
        void Delete(int id);
        void Delete(List<int> ids);
        Task DeleteAsync(int id);
        Task DeleteAsync(List<int> ids);
        void Delete(MasterProject model);
        void Delete(List<MasterProject> models);
        Task DeleteAsync(MasterProject model);
        Task DeleteAsync(List<MasterProject> models);
        void Save();
        Task SaveAsync();

    }
}
