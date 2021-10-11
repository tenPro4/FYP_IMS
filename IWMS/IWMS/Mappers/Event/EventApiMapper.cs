using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Event
{
    public static class EventApiMapper
    {
        internal static IMapper Mapper { get; set; }

        static EventApiMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EventApiMapperProfile>())
                .CreateMapper();
        }

        public static T ToEventModel<T>(this object e)
        {
            return Mapper.Map<T>(e);
        }

        public static List<T> ToEventListModel<T>(this object e)
        {
            return Mapper.Map<List<T>>(e);
        }
    }
}
