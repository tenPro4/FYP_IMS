using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Permission;
using BusinessLogic.Mapper;
using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IAsyncRepository<MasterDepartment> _departmentRepository;
        private readonly IAsyncRepository<EmployeeDepartment> _employeeDepartmentRep;
        private readonly IPermissionService _permissionService;

        public DepartmentService(
            IAsyncRepository<MasterDepartment> departmentRepository,
            IAsyncRepository<EmployeeDepartment> employeeDepartmentRep,
            IPermissionService permissionService
            )
        {
            _departmentRepository = departmentRepository;
            _employeeDepartmentRep = employeeDepartmentRep;
            _permissionService = permissionService;
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            var spec = new DepartmentSpecification(x => x.DepartmentId == id);
            var department = await _departmentRepository.GetSingleAsync(spec);
            var model =department.ToDepartmentModel<DepartmentDto>();

            return model;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var spec = new DepartmentSpecification();
            var department = await _departmentRepository.GetWithoutFilterAsync(spec);
            return department.ToDepartmentListModel<DepartmentDto>();
        }

        public async Task<DepartmentDto> GetByNameAsync(string name)
        {
            var department = await _departmentRepository.GetSingleAsync(x => x.DepartmentName == name);
            return department.ToDepartmentModel<DepartmentDto>();
        }

        public async Task<bool> AddAsync(DepartmentDto model)
        {
            if (await ExistAsync(model))
                return false;

            var entity = new MasterDepartment
            {
                DepartmentCode = model.DepartmentCode.ToUpper(),
                DepartmentName = model.DepartmentName.ToUpper()
            };
            return await _departmentRepository.AddAsync(entity) != null;
        }

        public async Task<bool> UpdateAsync(DepartmentDto model)
        {
            var department = await _departmentRepository.GetByIdAsync(model.DepartmentId);

            department.DepartmentName = model.DepartmentName.ToUpper();
            department.DepartmentCode = model.DepartmentCode.ToUpper();

            return await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            await _departmentRepository.DeleteAsync(department);
        }

        public async Task<bool> ExistAsync(DepartmentDto model)
        {
            if(await _departmentRepository.ExistsAsync(x => x.DepartmentName == model.DepartmentName.ToUpper()))
            {
                //throw exception
            }
            else if(await _departmentRepository.ExistsAsync(x => x.DepartmentName == model.DepartmentCode.ToUpper())){
                //throw exception
            }

            return false;
        }

        public async Task<DepartmentDto> JoinDepartmentWithUser(EmployeeDepartmentDto dto)
        {
            var exist = await _employeeDepartmentRep.ExistsAsync(x => x.EmployeeId == dto.EmployeeId && x.DepartmentId == dto.DepartmentId);
            if (exist)
                return null;

            var entity = await _employeeDepartmentRep.AddAsync(dto.ToDepartmentModel<EmployeeDepartment>());

            var dep =  await _departmentRepository.GetByIdAsync(dto.DepartmentId);
            var permissionId = await _permissionService.GetByName(dep.DepartmentName + ".READ");
            var empPermission = new EmployeePermissionDto
            {
                EmployeeId = dto.EmployeeId,
                PermissionId = permissionId.PermissionId
            };
            await _permissionService.AssignEmployeeAuthorizationAsync(empPermission);

            return dep.ToDepartmentModel<DepartmentDto>();
        }

        public async Task<bool> LeaveDepartmentWithUser(EmployeeDepartmentDto dto)
        {
            var exist = await _employeeDepartmentRep.ExistsAsync(x => x.EmployeeId == dto.EmployeeId && x.DepartmentId == dto.DepartmentId);

            if (exist)
            {
                //remove all authority
                var autList = await _permissionService.GetAllById(dto.EmployeeId);
                if (autList != null)
                    autList.ForEach(async x => await _permissionService.RemoveEmployeeAuthorizationAsync(x));

                if (await _employeeDepartmentRep.DeleteAsync(dto.ToDepartmentModel<EmployeeDepartment>()))
                    return true;
                //await AssignToUncategoryDepartmentAsync(dto.EmployeeId);


            }

            return false;
        }

        public async Task<DepartmentDto> AssignToUncategoryDepartmentAsync(int empId)
        {
            var uncategoryDepId = await GetByNameAsync("UNCATEGORY");

            if(empId != 0)
            {
                var empDep = new EmployeeDepartment
                {
                    EmployeeId = empId,
                    DepartmentId = uncategoryDepId.DepartmentId
                };

                return await JoinDepartmentWithUser(empDep.ToDepartmentModel<EmployeeDepartmentDto>());
            }

            return null;
        }
    }
}
