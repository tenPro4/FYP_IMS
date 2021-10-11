using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Permission
{
    public static class PermissionApiMapper
    {
        static PermissionApiMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<PermissionApiMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static T ToPermissionApiModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToPermissionApiListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
