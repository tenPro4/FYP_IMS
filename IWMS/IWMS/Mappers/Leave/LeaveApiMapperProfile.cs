using AutoMapper;
using BusinessLogic.Dtos.Leave;
using IWMS.Dtos.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Leave
{
    public class LeaveApiMapperProfile : Profile
    {
        public LeaveApiMapperProfile()
        {
            CreateMap<LeaveDto, LeaveApiDto>(MemberList.Destination);
            CreateMap<LeaveApiDto, LeaveDto>(MemberList.Destination)
                .ForMember(x => x.LeaveType, opt =>
                    opt.MapFrom(src => (int)src.LeaveType));

            CreateMap<SupportFileApiDto, SupportFileDto>(MemberList.Destination);
            CreateMap<SupportFileDto, SupportFileApiDto>(MemberList.Destination);
        }

    }
}
