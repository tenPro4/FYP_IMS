using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Department
{
    public static class DepartmentApiMapper
    {
        static DepartmentApiMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<DepartmentApiMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static T ToDepartmentApiModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToDepartmentApiListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
