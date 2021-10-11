using AutoMapper;
using BusinessLogic.Dtos.Log;
using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;

namespace BusinessLogic.Mapper
{
    public class LogMapperProfile:Profile
    {
        public LogMapperProfile()
        {
            CreateMap<Log, LogDto>(MemberList.Destination)
                .ReverseMap();

            CreateMap<PagedList<Log>, LogsDto>(MemberList.Destination)
                .ForMember(x => x.Logs,
                    opt => opt.MapFrom(src => src.Data));
        }
    }
}
