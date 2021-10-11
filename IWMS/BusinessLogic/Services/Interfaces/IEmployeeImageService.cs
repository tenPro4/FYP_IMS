using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeImageService
    {
        Task<EmployeeImageDto> GetByEmployeeId(int EmployeeId);
        Task<EmployeeImageDto> AddAsync(EmployeeImageDto model,int empId);
        Task<EmployeeImageDto> UpdateAsync(EmployeeImageDto model, int empId);
        Task<bool> ExistsAsync(int EmployeeId);
        Task DeleteAsync(int empId);
    }
}
