using AutoMapper;
using BusinessLogic.Dtos.Event;
using IWMS.Dtos.Event;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Event
{
    public class EventApiMapperProfile:Profile
    {
        public EventApiMapperProfile()
        {
            CreateMap<EventApiDto, EventDto>(MemberList.Destination)
                .ForMember(x => x.Start, opt =>
                    opt.MapFrom(src => DateTime.ParseExact(src.Start, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(x => x.End, opt =>
                    opt.MapFrom(src => DateTime.ParseExact(src.End, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));
        }
    }
}
