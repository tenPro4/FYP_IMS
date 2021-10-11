using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Employee
{
    public static class EmployeeApiMapper
    {
        static EmployeeApiMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeApiMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static T ToEmployeeApiModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToEmployeeApiListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
