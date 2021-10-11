using AutoMapper;
using BusinessLogic.Dtos.Attendance;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Leave;
using IWMS.Dtos.Attendance;
using IWMS.Dtos.Employee;
using IWMS.Dtos.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Attendance
{
    public class AttendanceMapperProfile : Profile
    {
        public AttendanceMapperProfile()
        {
            CreateMap<AttendanceDto, AttendanceApiDto>(MemberList.Destination);
            CreateMap<AttendanceApiDto, AttendanceDto>(MemberList.Destination);

            CreateMap<LeaveDto, LeaveApiDto>(MemberList.Destination);
            CreateMap<LeaveApiDto, LeaveDto>(MemberList.Destination)
                .ForMember(x => x.LeaveType, opt =>
                    opt.MapFrom(src => (int)src.LeaveType))
                 .ForMember(x => x.Start, opt =>
                  opt.MapFrom(src => Convert.ToDateTime(src.Start)))
                  .ForMember(x => x.End, opt =>
                  opt.MapFrom(src => Convert.ToDateTime(src.End)));

            CreateMap<EmployeeDto, EmployeeApiDto>(MemberList.Destination);
            CreateMap<EmployeeApiDto, EmployeeDto>(MemberList.Destination);

            CreateMap<SupportFileApiDto, SupportFileDto>(MemberList.Destination);
            CreateMap<SupportFileDto, SupportFileApiDto>(MemberList.Destination);
        }
    }
}
