using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Project
{
    public static class ProjectApiMapper
    {
        static ProjectApiMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProjectApiMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static T ToProjectApiModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToProjectApiListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
