using BusinessLogic.Dtos.Employee;
using BusinessLogic.Mapper;
using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class EmployeeAddressService:IEmployeeAddressService
    {
        private readonly IAsyncRepository<EmployeeAddress> _repository;

        public EmployeeAddressService(IAsyncRepository<EmployeeAddress> repository)
        {
            _repository = repository;
        }

        public virtual async Task<EmployeeAddressDto> AddAsync(EmployeeAddressDto model)
        {
            var entity = await _repository.AddAsync(model.ToEmployeeModel<EmployeeAddress>());
            return entity.ToEmployeeModel<EmployeeAddressDto>();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(int employeeId)
        {
            return await _repository.ExistsAsync(x => x.EmployeeId == employeeId);
        }

        public virtual async Task<EmployeeAddressDto> GetByEmployeeId(int employeeId)
        {
            var entity = await _repository.GetSingleAsync(x => x.EmployeeId == employeeId);
            return entity.ToEmployeeModel<EmployeeAddressDto>();
        }

        public virtual async Task UpdateAsync(int empId,EmployeeAddressDto model)
        {
            var entity = await _repository.GetSingleAsync(x => x.EmployeeId == empId);

            if (entity != null)
            {
                entity.HomeAddress = model.HomeAddress;
                entity.City = model.City;
                entity.Country = model.Country;
                entity.PostalCode = model.PostalCode;
                entity.ChangedDate = model.ChangedDate;

                await _repository.UpdateAsync(entity);
            }
        }
    }
}
