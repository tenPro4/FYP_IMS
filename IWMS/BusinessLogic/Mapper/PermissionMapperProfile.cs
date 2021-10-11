using AutoMapper;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Permission;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class PermissionMapperProfile : Profile
    {
        public PermissionMapperProfile()
        {
            CreateMap<MasterPermission, PermissionDto>(MemberList.Destination);
            CreateMap<PermissionDto, MasterPermission>(MemberList.Destination);

            CreateMap<EmployeePermission, EmployeePermissionDto>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore());
            CreateMap<EmployeePermissionDto, EmployeePermission>(MemberList.Destination);
        }
    }
}
