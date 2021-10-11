using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class LeaveMappers
    {
        internal static IMapper Mapper { get; set; }

        static LeaveMappers()
        {
            Mapper = new MapperConfiguration(
               cfg => cfg.AddProfile<LeaveMapperProfile>())
               .CreateMapper();
        }

        public static T ToLeaveModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }

        public static List<T> ToLeaveListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }
    }
}
