using AutoMapper;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Project;
using BusinessLogicShared.Constants;
using IWMS.Dtos.Employee;
using IWMS.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Project
{
    public class ProjectApiMapperProfile : Profile
    {
        public ProjectApiMapperProfile()
        {
            CreateMap<ProjectDto, ProjectApiDto>(MemberList.Destination)
            .ForMember(x => x.Status, opt =>
                    opt.MapFrom(src => (int)src.Status));
            CreateMap<ProjectApiDto, ProjectDto>(MemberList.Destination)
                .ForMember(x => x.Status, opt =>
                    opt.MapFrom(src => (ProjectStatus)src.Status));

            CreateMap<ProjectUserApiDto, ProjectUserDto>(MemberList.Destination);
            CreateMap<ProjectUserDto, ProjectUserApiDto>(MemberList.Destination);

            CreateMap<EmployeeDto, EmployeeApiDto>(MemberList.Destination);
            CreateMap<EmployeeApiDto, EmployeeDto>(MemberList.Destination);

            CreateMap<TaskCommentApiDto, TaskCommentDto>(MemberList.Destination);
            CreateMap<TaskCommentDto, TaskCommentApiDto>(MemberList.Destination);

            CreateMap<ProjectColumnDto, ProjectColumnApiDto>(MemberList.Destination);
            CreateMap<ProjectColumnApiDto, ProjectColumnDto>(MemberList.Destination);

            CreateMap<TaskApiDto, TaskDto>(MemberList.Destination)
                .ForMember(x => x.TaskPriority, opt =>
                    opt.MapFrom(src => (TaskPriority)src.TaskPriority))
                .ForMember(x => x.TaskType, opt =>
                    opt.MapFrom(src => Enum.Parse(typeof(TaskType), src.TaskType, true)))
                .ForMember(x => x.Assignees, opt =>
                    opt.MapFrom(src => src.Assignees.Select(
                        x => new TaskUserDto
                        {
                            EmployeeId = x
                        })));
            CreateMap<TaskDto, TaskApiDto>(MemberList.Destination)
                .ForMember(x => x.TaskPriority, opt =>
                    opt.MapFrom(src => (int)src.TaskPriority))
                .ForMember(x => x.TaskType, opt =>
                    opt.MapFrom(src => src.TaskType.ToString()));
        }
    }
}
