using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEmployeeAddressService
    {
        Task<bool> ExistsAsync(int EmployeeId);
        Task<EmployeeAddressDto> GetByEmployeeId(int EmployeeId);
        Task<EmployeeAddressDto> AddAsync(EmployeeAddressDto model);
        Task UpdateAsync(int empId,EmployeeAddressDto model);
        Task DeleteAsync(int id);
    }
}
