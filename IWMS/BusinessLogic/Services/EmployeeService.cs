using BusinessLogic.Dtos.Employee;
using BusinessLogic.Mapper;
using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogicShared.Filters;
using System.Linq;
using BusinessLogic.Dtos.Project;
using Microsoft.AspNetCore.Identity;
using BusinessLogic.Dtos.Role;
using BusinessLogic.Dtos.Permission;
using BusinessLogic.Dtos.Department;
using BusinessLogicShared.Common;

namespace BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAsyncRepository<Employee> _employeeRepository;
        private readonly IAsyncRepository<ProjectUser> projectUserRep;
        private readonly IEmployeeAddressService employeeAddressService;
        private readonly IEmployeeImageService employeeImageService;
        private readonly IProjectService projectService;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<MasterAccount> _userManager;
        private readonly IAsyncRepository<MasterAccount> _accountRep;
        private readonly IAsyncRepository<EmployeePermission> _permissionRep;
        private readonly IAsyncRepository<MasterDepartment> _departmentRep;
        private readonly IAsyncRepository<MasterPermission> _mPermissionRep;
        private readonly IAsyncRepository<EmployeeDepartment> _empDepRep;

        public EmployeeService(IAsyncRepository<Employee> employeeRepository,
            IEmployeeAddressService employeeAddressService,
            IEmployeeImageService employeeImageService,
            IProjectService projectService,
            IAsyncRepository<ProjectUser> projectUserRep,
            RoleManager<IdentityRole<int>> _roleManager,
            UserManager<MasterAccount> _userManager,
            IAsyncRepository<MasterAccount> _accountRep,
            IAsyncRepository<EmployeePermission> _permissionRep,
            IAsyncRepository<MasterDepartment> _departmentRep,
            IAsyncRepository<EmployeeDepartment> _empDepRep,
            IAsyncRepository<MasterPermission> _mPermissionRep
            )
        {
            _employeeRepository = employeeRepository;
            this.employeeAddressService = employeeAddressService;
            this.employeeImageService = employeeImageService;
            this.projectService = projectService;
            this.projectUserRep = projectUserRep;
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            this._accountRep = _accountRep;
            this._permissionRep = _permissionRep;
            this._departmentRep = _departmentRep;
            this._empDepRep = _empDepRep;
            this._mPermissionRep = _mPermissionRep;
        }

        public async Task<EmployeeDto> AddAsync(EmployeeDto model)
        {
            await _employeeRepository.AddAsync(model.ToEmployeeModel<Employee>());

            return model.ToEmployeeModel<EmployeeDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);
            await _employeeRepository.DeleteAsync(entity);
        }

        public async Task<bool> ExistsAsync(int EmployeeId)
        {
            return await _employeeRepository.ExistsAsync(x => x.EmployeeId == EmployeeId);
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var spec = new EmpSpecification();

            var employees = await _employeeRepository.GetWithoutFilterAsync(spec);
            var models = employees.ToEmployeeListModel<EmployeeDto>();

            foreach (var x in models)
            {
                x.EmployeeAddress = await employeeAddressService.GetByEmployeeId(x.EmployeeId);
                x.EmployeeImage = await employeeImageService.GetByEmployeeId(x.EmployeeId);
                var account = await _accountRep.GetByIdAsync(x.AccountId);
                var roles = await _userManager.GetRolesAsync(account);
                if (roles.Count > 0)
                {
                    x.Role = roles.FirstOrDefault();
                }

                if(x.Permission.Count > 0)
                {
                    var permissionSpec = new EmpPermissionSpecification(p => p.EmployeeId == x.EmployeeId);
                    var listEntity = await _permissionRep.GetAsync(permissionSpec);
                    x.Permission = listEntity.ToEmployeeListModel<EmployeePermissionDto>();
                }

                if (x.Department != null)
                {
                    var dep = await _departmentRep.GetByIdAsync(x.Department.DepartmentId);
                    x.Department.MasterDepartment = new DepartmentDto { DepartmentName = dep.DepartmentName,DepartmentCode = dep.DepartmentCode };
                }
            }
            return models;
        }

        public async Task<List<EmployeeDto>> GetAsync(EmployeeFilter filter)
        {
            var spec = new EmpSpecification(x =>
            (!filter.DepartmentId.HasValue || x.Department.DepartmentId == filter.DepartmentId)
              && (string.IsNullOrEmpty(filter.EmployeeName) || (x.FirstName.Contains(filter.EmployeeName))
             && ( !filter.EmployeeId.HasValue || x.EmployeeId == filter.EmployeeId)
             && (string.IsNullOrEmpty(filter.Gender) || x.Gender == filter.Gender)));

            var employees = await _employeeRepository.GetAsync(spec);
            var models = employees.ToEmployeeListModel<EmployeeDto>();

            foreach (var x in models)
            {
                x.EmployeeAddress = await employeeAddressService.GetByEmployeeId(x.EmployeeId);
                x.EmployeeImage = await employeeImageService.GetByEmployeeId(x.EmployeeId);
            }
            return models;
        }

        public virtual async Task<EmployeeDto> GetByAccountIdAsync(int accId)
        {
            var employee = await _employeeRepository.GetSingleAsync(x => x.AccountId == accId);

            return employee.ToEmployeeModel<EmployeeDto>();
        }

        public async Task<EmployeeDto> GetByEmployeeIdAsync(int employeeId)
        {
            var spec = new EmpSpecification(x => x.EmployeeId == employeeId);
            var emp = await _employeeRepository.GetSingleAsync(spec);
            var model = emp.ToEmployeeModel<EmployeeDto>();
                
            //model.EmployeeImage = await employeeImageService.GetByEmployeeId(model.EmployeeId);
            //model.EmployeeAddress = await employeeAddressService.GetByEmployeeId(model.EmployeeId);

            return model;
        }

        public async Task<List<EmployeeDto>> GetNoProjectMemberAsync(int projectId)
        {
            var spec =new EmpSpecification();

            var all = await _employeeRepository.GetWithoutFilterAsync(spec);

            var existMember = await projectUserRep.GetAsync(x => x.ProjectId == projectId);

            var filter = all.Where(x => !existMember.Select(y => y.EmployeeId).Contains(x.EmployeeId)).ToList();

            return filter.ToEmployeeListModel<EmployeeDto>();
        }

        public async Task<EmployeeDto> UpdateEmployeePermission(CommonRequest req)
        {
            var listRoles = new[] { "Admin", "Executive"};
            //role
            var spec = new EmpSpecification(x => x.EmployeeId == req.EmployeeId);
            var entity = await _employeeRepository.GetSingleWithNoTrackAsync(spec);
            var account = await _accountRep.GetSingleWithNoTrackAsync(x => x.Id == entity.AccountId);
            //if (!await _userManager.IsInRoleAsync(account, req.Role))
            //{
            //    await _userManager.AddToRoleAsync(account, req.Role);
            //    await _userManager.RemoveFromRoleAsync(account, req.OldRole);
            //}

            //department
            var departmentEntity = await _departmentRep.GetSingleWithNoTrackAsync(x => x.DepartmentName.Equals(req.Department, StringComparison.InvariantCultureIgnoreCase));
            if(departmentEntity != null && departmentEntity.DepartmentId != entity.Department.DepartmentId)
            {
                var empDep =await _empDepRep.GetSingleWithNoTrackAsync(x => x.EmployeeId == entity.EmployeeId);
                await _empDepRep.DeleteAsync(empDep);

                await _empDepRep.AddAsync(new EmployeeDepartment {
                    DepartmentId = departmentEntity.DepartmentId,
                    EmployeeId = req.EmployeeId
                });
            }

            //permission
            var permissionSpec = new EmpPermissionSpecification(x => x.EmployeeId == entity.EmployeeId);
            var existPermissions = await _permissionRep.GetWithNoTrackAsync(permissionSpec);
            foreach(var permission in existPermissions)
            {
                var exists = req.Permission.Contains(permission.MasterPermission.PermissionName);

                if (!exists)
                {
                    await _permissionRep.DeleteAsync(permission);
                }
            }

            foreach(var add in req.Permission)
            {
                if(!existPermissions.Select(x => x.MasterPermission.PermissionName).Contains(add))
                {
                    var permission = await _mPermissionRep.GetSingleAsync(x => x.PermissionName == add);
                    await _permissionRep.AddAsync(new EmployeePermission
                    {
                        EmployeeId = req.EmployeeId,
                        PermissionId = permission.PermissionId
                    });
                }
            }
            var employee = await _employeeRepository.GetSingleAsync(spec);
            var result = employee.ToEmployeeModel<EmployeeDto>();
            

            var depEntity = await _departmentRep.GetSingleAsync(x => x.DepartmentId == result.Department.DepartmentId);
            result.Department.MasterDepartment = depEntity.ToEmployeeModel<DepartmentDto>();
            result.EmployeeImage = await employeeImageService.GetByEmployeeId(result.EmployeeId);
            var ed = new EmpPermissionSpecification(x => x.EmployeeId == result.EmployeeId);
            var listPermission = await _permissionRep.GetAsync(ed);
            result.Permission = listPermission.ToEmployeeListModel<EmployeePermissionDto>();
            result.Role = req.Role;
            

            return result.ToEmployeeModel<EmployeeDto>();
        }

        public async Task UpdateAsync(EmployeeDto model,int empId)
        {
            var entity = await _employeeRepository.GetByIdAsync(empId);

            if(entity != null)
            {
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Gender = model.Gender;
                //entity.BirthDate = model.BirthDate;
                entity.Description = model.Description;
                entity.ChangedDate = DateTime.Now;
                entity.PhoneNumber = model.PhoneNumber;
                //entity.EmployeeAddress = model.EmployeeAddress.ToEmployeeModel<EmployeeAddress>();
            }

            await _employeeRepository.UpdateAsync(entity);
        }
    }
}
