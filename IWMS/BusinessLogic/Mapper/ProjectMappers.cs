using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class ProjectMappers
    {
        internal static IMapper Mapper { get; set; }

        static ProjectMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProjectMapperProfile>())
                .CreateMapper();
        }

        public static T ToProjectModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToProjectListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
