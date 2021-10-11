using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Attendance
{
    public static class AttendanceMappers
    {
        internal static IMapper Mapper { get; set; }

        static AttendanceMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AttendanceMapperProfile>())
                .CreateMapper();
        }

        public static T ToAttendanceApiModel<T>(this object att)
        {
            return Mapper.Map<T>(att);
        }
    }
}
