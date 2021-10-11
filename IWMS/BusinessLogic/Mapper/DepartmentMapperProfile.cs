using AutoMapper;
using BusinessLogic.Dtos.Department;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class DepartmentMapperProfile:Profile
    {
        public DepartmentMapperProfile()
        {
            CreateMap<MasterDepartment, DepartmentDto>(MemberList.Destination);
            CreateMap<DepartmentDto, MasterDepartment>(MemberList.Destination);
            CreateMap<EmployeeDepartment, EmployeeDepartmentDto>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore());
            CreateMap<EmployeeDepartmentDto, EmployeeDepartment>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore()); ;
        }
    }
}
