using AutoMapper;
using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Leave;
using BusinessLogic.Extensions;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class LeaveMapperProfile:Profile
    {
        public LeaveMapperProfile()
        {
            CreateMap<Leave, LeaveDto>(MemberList.Destination);
            CreateMap<LeaveDto, Leave>(MemberList.Destination);

            CreateMap<SupportFile, SupportFileDto>(MemberList.Destination);
            CreateMap<SupportFileDto, SupportFile>(MemberList.Destination);

            CreateMap<MasterAccount, AccountDto>(MemberList.Destination)
               .ForMember(x => x.Remember, opt => opt.Ignore())
               .ForMember(x => x.FirstName, opt => opt.Ignore())
               .ForMember(x => x.LastName, opt => opt.Ignore())
               .IgnoreNoMap();
            CreateMap<AccountDto, MasterAccount>(MemberList.Destination);

            CreateMap<Employee, EmployeeDto>(MemberList.Destination);
            CreateMap<EmployeeDto, Employee>(MemberList.Destination);

            //CreateMap<MasterDepartment, DepartmentDto>(MemberList.Destination);
            //CreateMap<DepartmentDto, MasterDepartment>(MemberList.Destination);
        }
    }
}
