using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<List<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByNameAsync(string name);
        Task<bool> AddAsync(DepartmentDto model);
        Task<bool> UpdateAsync(DepartmentDto model);
        Task<bool> ExistAsync(DepartmentDto name);
        Task DeleteAsync(int id);
        Task<DepartmentDto> AssignToUncategoryDepartmentAsync(int empId);
        Task<DepartmentDto> JoinDepartmentWithUser(EmployeeDepartmentDto dto);
        Task<bool> LeaveDepartmentWithUser(EmployeeDepartmentDto dto);
    }
}
