using AutoMapper;
using BusinessLogic.Dtos.Permission;
using IWMS.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Permission
{
    public class PermissionApiMapperProfile:Profile
    {
        public PermissionApiMapperProfile()
        {
            CreateMap<PermissionApiDto, PermissionDto>(MemberList.Destination);
            CreateMap<PermissionDto, PermissionApiDto>(MemberList.Destination);

            CreateMap<EmployeePermissionApiDto, EmployeePermissionDto>(MemberList.Destination)
                .ForMember(x => x.Employee, opt => opt.Ignore());
            CreateMap<EmployeePermissionDto, EmployeePermissionApiDto>(MemberList.Destination);
        }
    }
}
