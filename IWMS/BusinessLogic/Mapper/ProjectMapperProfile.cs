using AutoMapper;
using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Dtos.Project;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Mapper
{
    public class ProjectMapperProfile:Profile
    {
        public ProjectMapperProfile()
        {
            CreateMap<MasterProject, ProjectDto>(MemberList.Destination);
            CreateMap<ProjectDto, MasterProject>(MemberList.Destination);

            CreateMap<ProjectColumn, ProjectColumnDto>(MemberList.Destination);
            CreateMap<ProjectColumnDto, ProjectColumn>(MemberList.Destination);

            CreateMap<MasterTask, TaskDto>(MemberList.Destination);
            CreateMap<TaskDto, MasterTask>(MemberList.Destination)
                .ForMember(x => x.ListPosition, opt => opt.Ignore())
                .ForMember(x => x.Assignees, opt => opt.Ignore());

            //CreateMap<TaskStatus, TaskStatusDto>(MemberList.Destination);
            //CreateMap<TaskStatusDto, TaskStatus>(MemberList.Destination);

            //CreateMap<TaskPriority, TaskPriorityDto>(MemberList.Destination);
            //CreateMap<TaskPriorityDto, TaskPriority>(MemberList.Destination);

            CreateMap<Employee, EmployeeDto>(MemberList.Destination);
            CreateMap<EmployeeDto, Employee>(MemberList.Destination);

            CreateMap<EmployeeAddress, EmployeeAddressDto>(MemberList.Destination);
            CreateMap<EmployeeAddressDto, EmployeeAddress>(MemberList.Destination);

            CreateMap<EmployeeImage, EmployeeImageDto>(MemberList.Destination);
            CreateMap<EmployeeImageDto, EmployeeImage>(MemberList.Destination);

            CreateMap<EmployeeDepartment, EmployeeDepartmentDto>(MemberList.Destination);
            CreateMap<EmployeeDepartmentDto, EmployeeDepartment>(MemberList.Destination);

            CreateMap<EmployeeImage, EmployeeImageDto>(MemberList.Destination);
            CreateMap<EmployeeImageDto, EmployeeImage>(MemberList.Destination);

            CreateMap<ProjectUser, ProjectUserDto>(MemberList.Destination);
            CreateMap<ProjectUserDto, ProjectUser>(MemberList.Destination);

            CreateMap<TaskComment, TaskCommentDto>(MemberList.Destination);
            CreateMap<TaskCommentDto, TaskComment>(MemberList.Destination);

            CreateMap<TaskUser, TaskUserDto>(MemberList.Destination);
            CreateMap<TaskUserDto,TaskUser>(MemberList.Destination);
        }
    }
}

