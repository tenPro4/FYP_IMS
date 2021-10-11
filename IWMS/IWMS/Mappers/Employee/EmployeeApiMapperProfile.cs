using AutoMapper;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Project;
using IWMS.Dtos.Department;
using IWMS.Dtos.Employee;
using IWMS.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Employee
{
    public class EmployeeApiMapperProfile:Profile
    {
        public EmployeeApiMapperProfile()
        {
            CreateMap<EmployeeDto, EmployeeApiDto>(MemberList.Destination);
            CreateMap<EmployeeApiDto, EmployeeDto>(MemberList.Destination);

            CreateMap<EmployeeAddressApiDto, EmployeeAddressDto>(MemberList.Destination);
            CreateMap<EmployeeAddressDto, EmployeeAddressApiDto>(MemberList.Destination);

            CreateMap<EmployeeImageApiDto, EmployeeImageDto>(MemberList.Destination);
            CreateMap<EmployeeImageDto, EmployeeImageApiDto>(MemberList.Destination);

            CreateMap<DepartmentApiDto, DepartmentDto>(MemberList.Destination);
            CreateMap<DepartmentDto, DepartmentApiDto>(MemberList.Destination)
                .ForMember(x => x.Employees, opt => opt.Ignore());

            CreateMap<EmployeeDepartmentApiDto, EmployeeDepartmentDto>(MemberList.Destination);
            CreateMap<EmployeeDepartmentDto, EmployeeDepartmentApiDto>(MemberList.Destination);

            CreateMap<ProjectApiDto, ProjectDto>(MemberList.Destination);
            CreateMap<ProjectDto, ProjectApiDto>(MemberList.Destination);

            CreateMap<ProjectUserApiDto, ProjectUserDto>(MemberList.Destination);
            CreateMap<ProjectUserDto, ProjectUserApiDto>(MemberList.Destination);

        }
    }
}
