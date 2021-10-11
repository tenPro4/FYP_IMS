using BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Permission;
using System.Threading.Tasks;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Entities;
using BusinessLogic.Mapper;
using BusinessLogic.Dtos.Department;
using EntityFramework.Specifications;

namespace BusinessLogic.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IAsyncRepository<MasterPermission> asyncRepository;
        private readonly IAsyncRepository<EmployeePermission> employeePermissionRep;

        public PermissionService(
            IAsyncRepository<MasterPermission> asyncRepository,
            IAsyncRepository<EmployeePermission> employeePermissionRep)
        {
            this.asyncRepository = asyncRepository;
            this.employeePermissionRep = employeePermissionRep;
        }

        public async Task<PermissionDto> Add(PermissionDto dto)
        {
            var entity =await asyncRepository.AddAsync(dto.ToPermissionModel<MasterPermission>());

            return entity.ToPermissionModel<PermissionDto>();
        }

        public async Task<EmployeePermissionDto> AssignEmployeeAuthorizationAsync(EmployeePermissionDto dto)
        {
            if (await GetById(dto.PermissionId) == null)
                throw new Exception();

            var entity = new EmployeePermission
            {
                EmployeeId = dto.EmployeeId,
                PermissionId = dto.PermissionId
            };

            var result= await employeePermissionRep.AddAsync(entity);
            return result.ToPermissionModel<EmployeePermissionDto>();
        }

        public async Task<bool> CanAssignOrRemovePermission(EmployeePermissionDto dto)
        {
            return await employeePermissionRep.ExistsAsync(x => x.EmployeeId == dto.EmployeeId && x.PermissionId == dto.PermissionId);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await asyncRepository.GetByIdAsync(id);
            if (entity != null)
                return await asyncRepository.DeleteAsync(entity);

            return false;
        }

        public async Task<bool> Exists(string name)
        {
            return await asyncRepository.ExistsAsync(x => x.PermissionName == name.ToUpper() || x.PermissionCode == name.ToUpper());
        }

        public async Task<PermissionDto> GetById(int id)
        {
            var entity = await asyncRepository.GetSingleAsync(x => x.PermissionId == id);

            if (entity != null)
                return entity.ToPermissionModel<PermissionDto>();

            return null;
        }

        public async Task<List<PermissionDto>> GetAll()
        {
            var entity = await asyncRepository.GetAllAsync();

            return entity.ToPermissionListModel<PermissionDto>();
        }

        public async Task<List<EmployeePermissionDto>> GetAllById(int empId)
        {
            if (!await employeePermissionRep.ExistsAsync(x => x.EmployeeId == empId))
                return null;

            var spec = new EmpPermissionSpecification(x => x.EmployeeId == empId);
            var permissionList = await employeePermissionRep.GetAsync(spec);

            return permissionList.ToPermissionListModel<EmployeePermissionDto>();
        }

        public async Task<bool> RemoveEmployeeAuthorizationAsync(EmployeePermissionDto dto)
        {
            var entity = await employeePermissionRep
                .GetSingleAsync(x => x.PermissionId == dto.PermissionId && x.EmployeeId == dto.EmployeeId);
            return await employeePermissionRep.DeleteAsync(entity);
        }

        public async Task<bool> Update(PermissionDto dto)
        {
            var per = await GetById(dto.PermissionId);
            per.PermissionName = dto.PermissionName.ToUpper();
            per.PermissionCode = dto.PermissionCode.ToUpper();
            return await asyncRepository.UpdateAsync(per.ToPermissionModel<MasterPermission>());
        }

        public async Task<PermissionDto> GetByName(string name)
        {
            var entity = await asyncRepository.GetSingleAsync(x => x.PermissionName == name.ToUpper());

            return entity.ToPermissionModel<PermissionDto>();
        }
    }
}
