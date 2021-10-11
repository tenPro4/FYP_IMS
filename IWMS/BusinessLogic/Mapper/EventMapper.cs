using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class EventMapper
    {
        internal static IMapper Mapper { get; set; }

        static EventMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EventMapperProfile>())
               .CreateMapper();
        }

        public static T ToEventModel<T>(this object att)
        {
            return Mapper.Map<T>(att);
        }

        public static List<T> ToEventListModel<T>(this object atts)
        {
            return Mapper.Map<List<T>>(atts);
        }
    }
}
