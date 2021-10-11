using AutoMapper;
using BusinessLogic.Dtos.Attendance;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class AttendanceMapperProfile:Profile
    {
        public AttendanceMapperProfile()
        {
            CreateMap<Attendance, AttendanceDto>(MemberList.Destination);
            CreateMap<AttendanceDto, Attendance>(MemberList.Destination);
        }
    }
}
