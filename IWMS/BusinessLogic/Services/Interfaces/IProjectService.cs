using BusinessLogic.Dtos.Project;
using BusinessLogicShared.Common;
using BusinessLogicShared.Request;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetProjectAsync(int id);
        Task<List<ProjectDto>> GetProjectsAsync();
        Task<ProjectDto> InsertProjectAsync(AddProjectRequest req);
        Task<bool> DeleteProjectAsync(int id);
        Task UpdateProjectAsync(int id, ProjectDto dto);
        Task<bool> ExistsAsync(int id);
        Task<ProjectUserDto> JoinProjectAsync(ProjectUserDto dto);
        Task LeaveProjectAsync(ProjectUserDto dto);
        Task<ProjectColumnDto> AddColumn(int id);
        Task DeleteColumn(int id);
        Task<List<ProjectUserDto>> AddProjectMembersAsync(int id,ProjectUserRequest req);
        Task UpdateProjectColumnTitleAsync(int id, ProjectColumnDto dto);
        Task UpdateProjectColumnOrderAsync(int id, ProjectColumnDto dto);
        Task<ProjectOverview> ProjectOverviewAsync(int employeeId);
    }
}
