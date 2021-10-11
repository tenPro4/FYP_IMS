using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionDto> Add(PermissionDto dto);
        Task<bool> Update(PermissionDto dto);
        Task<bool> Delete(int id);
        Task<bool> Exists(string name);
        Task<PermissionDto> GetById(int id);
        Task<PermissionDto> GetByName(string name);
        Task<List<PermissionDto>> GetAll();
        Task<List<EmployeePermissionDto>> GetAllById(int empId);
        Task<bool> RemoveEmployeeAuthorizationAsync(EmployeePermissionDto dto);
        Task<EmployeePermissionDto> AssignEmployeeAuthorizationAsync(EmployeePermissionDto dto);
        Task<bool> CanAssignOrRemovePermission(EmployeePermissionDto dto);
    }
}
