using AutoMapper;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Permission;
using BusinessLogic.Dtos.Project;
using BusinessLogic.Dtos.Role;
using EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Mapper
{
    public class EmployeeMapperProfile:Profile
    {
        public EmployeeMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>(MemberList.Destination);
            CreateMap<EmployeeDto, Employee>(MemberList.Destination);

            CreateMap<MasterDepartment, DepartmentDto>(MemberList.Destination);
            CreateMap<DepartmentDto, MasterDepartment>(MemberList.Destination);

            CreateMap<EmployeeAddress, EmployeeAddressDto>(MemberList.Destination);
            CreateMap<EmployeeAddressDto, EmployeeAddress>(MemberList.Destination);

            CreateMap<EmployeeImage, EmployeeImageDto>(MemberList.Destination);               
            CreateMap<EmployeeImageDto, EmployeeImage>(MemberList.Destination);

            CreateMap<EmployeeDepartment, EmployeeDepartmentDto>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore());
            CreateMap<EmployeeDepartmentDto, EmployeeDepartment>(MemberList.Destination);

            CreateMap<PermissionDto, MasterPermission>(MemberList.Destination);
            CreateMap<MasterPermission, PermissionDto>(MemberList.Destination);

            CreateMap<MasterProject, ProjectDto>(MemberList.Destination);
            CreateMap<ProjectDto, MasterProject>(MemberList.Destination);

            CreateMap<ProjectUser, ProjectUserDto>(MemberList.Destination);
            CreateMap<ProjectUserDto, ProjectUser>(MemberList.Destination);

            CreateMap<MasterTask, TaskDto>(MemberList.Destination)
                .ForAllMembers(x => x.Ignore());
            CreateMap<TaskDto, MasterTask>(MemberList.Destination)
                .ForAllMembers(x => x.Ignore());

            CreateMap<EmployeePermission, EmployeePermissionDto>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore());
            CreateMap<EmployeePermissionDto, EmployeePermission>(MemberList.Destination);
        }
    }
}

