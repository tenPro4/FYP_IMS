using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class DepartmentMapper
    {
        internal static IMapper Mapper { get; set; }

        static DepartmentMapper()
        {
            Mapper = new MapperConfiguration(
                cfg => cfg.AddProfile<DepartmentMapperProfile>())
                .CreateMapper();
        }

        public static List<T> ToDepartmentListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }

        public static T ToDepartmentModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }
    }
}
