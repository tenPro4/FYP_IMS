using AutoMapper;
using BusinessLogic.Dtos.Employee;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class EmployeeImageMapperProfile:Profile
    {
        public EmployeeImageMapperProfile()
        {
            CreateMap<EmployeeImage, EmployeeImageDto>(MemberList.Destination);

            CreateMap<EmployeeImageDto, EmployeeImage>(MemberList.Destination);
        }   
    }
}
