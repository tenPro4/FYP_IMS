using AutoMapper;
using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using IWMS.Dtos;
using IWMS.Dtos.Account;
using IWMS.Dtos.Department;
using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Department
{
    public class DepartmentApiMapperProfile:Profile
    {
        public DepartmentApiMapperProfile()
        {
            CreateMap<DepartmentApiDto, DepartmentDto>(MemberList.Destination);
            CreateMap<DepartmentDto, DepartmentApiDto>(MemberList.Destination)
                .ForMember(x => x.Employees, opt => opt.Ignore());

            CreateMap<EmployeeDepartmentApiDto, EmployeeDepartmentDto>(MemberList.Destination);
            CreateMap<EmployeeDepartmentDto, EmployeeDepartmentApiDto>(MemberList.Destination);

            CreateMap<AccountDto, AccountApiDto>(MemberList.Destination);
            CreateMap<AccountApiDto, AccountDto>(MemberList.Destination);

            CreateMap<EmployeeDto, EmployeeApiDto>(MemberList.Destination);
            CreateMap<EmployeeApiDto, EmployeeDto>(MemberList.Destination);


        }
    }
}
