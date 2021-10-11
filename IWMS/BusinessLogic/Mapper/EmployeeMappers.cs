using AutoMapper;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class EmployeeMappers
    {
        static EmployeeMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper;

        public static T ToEmployeeModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToEmployeeListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
