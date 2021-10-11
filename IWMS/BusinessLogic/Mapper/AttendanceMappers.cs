using AutoMapper;
using BusinessLogic.Dtos.Attendance;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class AttendanceMappers
    {
        internal static IMapper Mapper { get; set; }

        static AttendanceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AttendanceMapperProfile>())
                .CreateMapper();
        }

        public static T ToAttendanceModel<T>(this object att)
        {
            return Mapper.Map<T>(att);
        }

        public static List<T> ToAttendanceListModel<T>(this object atts)
        {
            return Mapper.Map<List<T>>(atts);
        }
    }
}
