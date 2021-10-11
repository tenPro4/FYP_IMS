using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Project;
using BusinessLogic.Helpers;
using BusinessLogicShared.Common;
using BusinessLogicShared.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();
        //Task<List<EmployeeDto>> GetByNameAsync(string name);
        Task<List<EmployeeDto>> GetAsync(EmployeeFilter filter);
        Task<EmployeeDto> GetByEmployeeIdAsync(int EmployeeId);
        Task<EmployeeDto> GetByAccountIdAsync(int accId);
        Task<EmployeeDto> UpdateEmployeePermission(CommonRequest req); 
        //Task<EmployeeDto> GetByEmployeeIdWithDetailAsync(int EmployeeId);
        //Task<int> CountTotalEmployeeAsync();
        Task<bool> ExistsAsync(int EmployeeId);
        Task<EmployeeDto> AddAsync(EmployeeDto model);
        Task UpdateAsync(EmployeeDto model,int empId);
        Task DeleteAsync(int id);
        Task<List<EmployeeDto>> GetNoProjectMemberAsync(int projectId);
    }
}
