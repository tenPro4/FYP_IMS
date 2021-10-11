using BusinessLogic.Dtos.Project;
using BusinessLogic.Helpers;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Request;
using IWMS.Dtos.Project;
using IWMS.Helpers;
using IWMS.Mappers.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskController:Controller
    {
        private readonly ITaskService taskService;
        private readonly IEmployeeService employeeService;

        public TaskController(ITaskService taskService,
            IEmployeeService employeeService
            )
        {
            this.taskService = taskService;
            this.employeeService = employeeService;
        }

        [HttpPost("api/task/addTask/{id}")]
        public async Task<IActionResult> AddTask([FromRoute]int id,[FromBody]TaskApiDto t)
        {
            t.CreatedByUserId = Int32.Parse(HttpContext.GetEmpId());
            t.Title = t.Title.Trim().Equals("") ? "New Task" : t.Title;
            var dto = t.ToProjectApiModel<TaskDto>();
            var result = await taskService.AddTaskAsync(id,dto);
            return Ok(result);
        }

        [HttpPost("api/task/sortTask")]
        public async Task<IActionResult> SortTask([FromBody]SortTaskRequest req)
        {
            await taskService.SortingTaskOrder(req);

            return Ok();
        }

        [HttpPost("api/task/addComment/{id}")]
        public async Task<IActionResult> AddComment([FromRoute]int id, [FromBody]TaskCommentDto comment)
        {
            comment.EmployeeId = Int32.Parse(HttpContext.GetEmpId());
            return Ok(await taskService.AddCommentAsync(id, comment));
        }

        [HttpDelete("api/task/deleteComment/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute]int id)
        {
            await taskService.DeleteCommentAsync(id);
            return Ok();
        }

        [HttpDelete("api/task/removeTask/{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute]int id)
        {
            return Ok(await taskService.DeleteAsync(id));
        }

        [HttpGet("api/task/projectTasks/{projectId}")]
        public async Task<IActionResult> GetTasks([FromRoute]int projectId)
        {
            var dto = await taskService.GetTaskBaseColumnAsync(projectId);
            return Ok(dto.ToProjectApiListModel<TaskApiDto>());
        }

        [HttpGet("api/task/{id}")]
        public async Task<IActionResult> GetTaskById([FromRoute]int id)
        {
            var dto = await taskService.GetByIdAsync(id);
            return Ok(dto.ToProjectApiModel<TaskApiDto>());
        }

        [HttpPatch("api/task/updateTask/{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute]int id,[FromBody]TaskApiDto t)
        {
            var result = await taskService.UpdateAsync(id,t.ToProjectApiModel<TaskDto>());
            return Ok(result);
        }

        [HttpPatch("api/task/updateTaskAssignee/{id}")]
        public async Task<IActionResult> UpdateTaskAssignee([FromRoute]int id, [FromBody]TaskApiDto t)
        {
            var result = await taskService.UpdateAssignees(id, t.ToProjectApiModel<TaskDto>());
            return Ok(result);
        }
    }
}
