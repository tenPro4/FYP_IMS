using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class EmployeeImageMappers
    {
        static EmployeeImageMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeImageMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper;

        public static T ToEmployeeImageEntityDto<T>(this object img)
        {
            return Mapper.Map<T>(img);
        }
    }
}
