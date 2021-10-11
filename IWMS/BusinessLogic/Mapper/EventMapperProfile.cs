using AutoMapper;
using BusinessLogic.Dtos.Event;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class EventMapperProfile:Profile
    {
        public EventMapperProfile()
        {
            CreateMap<Event, EventDto>(MemberList.Destination);
            CreateMap<EventDto, Event>(MemberList.Destination);
        }
    }
}
