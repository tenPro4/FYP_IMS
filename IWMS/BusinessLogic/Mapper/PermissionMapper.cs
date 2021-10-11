using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class PermissionMapper
    {
        internal static IMapper Mapper { get; set; }

        static PermissionMapper()
        {
            Mapper = new MapperConfiguration(cfg => 
            cfg.AddProfile<PermissionMapperProfile>())
                .CreateMapper();
        }

        public static List<T> ToPermissionListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }

        public static T ToPermissionModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }
    }
}
