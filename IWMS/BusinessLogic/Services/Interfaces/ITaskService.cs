using BusinessLogic.Dtos.Project;
using BusinessLogic.Helpers;
using BusinessLogicShared.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> AddTaskAsync(int id,TaskDto task);
        Task<bool> DeleteAsync(int id);
        Task<TaskDto> GetByIdAsync(int id);
        Task<List<TaskDto>> GetTaskBaseColumnAsync(int projectId);
        Task<TaskDto> UpdateAsync(int id,TaskDto dto);
        Task<TaskCommentDto> AddCommentAsync(int id,TaskCommentDto comment);
        Task DeleteCommentAsync(int id);
        Task SortingTaskOrder(SortTaskRequest sort);
        Task<TaskDto> UpdateAssignees(int id, TaskDto dto);
    }
}
