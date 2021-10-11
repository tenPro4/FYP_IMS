using AutoMapper;
using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Permission;
using BusinessLogic.Dtos.Project;
using BusinessLogic.Extensions;
using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<MasterAccount, AccountDto>(MemberList.Destination)
                .ForMember(x => x.Remember, opt => opt.Ignore())
                .ForMember(x => x.FirstName,opt => opt.Ignore())
                .ForMember(x => x.LastName, opt => opt.Ignore())
                .IgnoreNoMap();
            CreateMap<AccountDto, MasterAccount>(MemberList.Destination);

            CreateMap<PagedList<MasterAccount>, AccountsDto>(MemberList.Destination)
                .ForMember(x => x.Accounts,
                    opt => opt.MapFrom(src => src.Data));

            CreateMap<Employee, EmployeeDto>(MemberList.Destination);
            CreateMap<EmployeeDto, Employee>(MemberList.Destination);

            CreateMap<MasterDepartment, DepartmentDto>(MemberList.Destination);
            CreateMap<DepartmentDto, MasterDepartment>(MemberList.Destination);

            CreateMap<EmployeeDepartment, EmployeeDepartmentDto>(MemberList.Destination);
            CreateMap<EmployeeDepartmentDto, EmployeeDepartment>(MemberList.Destination);

            CreateMap<EmployeePermission, EmployeePermissionDto>(MemberList.Destination);
            CreateMap<EmployeePermissionDto, EmployeePermission>(MemberList.Destination);

            CreateMap<MasterProject, ProjectDto>(MemberList.Destination);
            CreateMap<ProjectDto, MasterProject>(MemberList.Destination);

            CreateMap<ProjectUser, ProjectUserDto>(MemberList.Destination);
            CreateMap<ProjectUserDto, ProjectUser>(MemberList.Destination);

            //refreshToken
            CreateMap<RefreshToken, RefreshTokenDto>(MemberList.Destination);
            CreateMap<RefreshTokenDto, RefreshToken>(MemberList.Destination);
        }

    }
}
