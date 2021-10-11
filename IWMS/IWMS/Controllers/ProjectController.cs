using BusinessLogic.Dtos.Project;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Request;
using EntityFramework.Entities;
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
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly ITaskService taskService;

        public ProjectController(IProjectService projectService, ITaskService taskService)
        {
            this.projectService = projectService;
            this.taskService = taskService;
        }

        [HttpGet("api/project/all")]
        public async Task<IActionResult> Get()
        {
            return Ok(await projectService.GetProjectsAsync());
        }

        [HttpGet("api/project/overview")]
        public async Task<IActionResult> GetOverview()
        {
            var overview = await projectService.ProjectOverviewAsync(Int32.Parse(HttpContext.GetEmpId()));
            //var overview = await projectService.ProjectOverviewAsync(3);
            return Ok(overview);
        }

        [HttpGet("api/project/get/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            return Ok(await projectService.GetProjectAsync(id));
        }

        [HttpPost("api/project/addone")]
        public async Task<IActionResult> AddOne([FromBody]AddProjectRequest req)
        {
            if (req == null)
                return BadRequest();

            req.EmployeeLeaderId =Int32.Parse(HttpContext.GetEmpId());
            var model = await projectService.InsertProjectAsync(req);

            return Ok(model);
        }

        [HttpDelete("api/project/delete/{id}")]
        public async Task<IActionResult> DeleteOne([FromRoute]int id)
        {
            if (await projectService.DeleteProjectAsync(id))
                return NoContent();

            return NotFound();
        }

        [HttpPost("api/project/addColumn/{id}")]
        public async Task<IActionResult> AddColumn([FromRoute]int id)
        {
            if (id == 0)
                return BadRequest();

            var model = await projectService.AddColumn(id);

            return Ok(model);
        }

        [HttpDelete("api/project/deleteColumn/{id}")]
        public async Task<IActionResult> DeleteColumn([FromRoute]int id)
        {
            if (id == 0)
                return BadRequest();

            await projectService.DeleteColumn(id);

            return Ok();
        }

        [HttpPost("api/project/joinProject/{id}")]
        public async Task<IActionResult> JoinProject([FromRoute]int id,[FromBody]ProjectUserRequest req)
        {
            var dto = new ProjectUserDto
            {
                EmployeeId = req.EmployeeId,
                ProjectId = req.ProjectId
            };
            return Ok(await projectService.JoinProjectAsync(dto));
        }

        [HttpDelete("api/project/leaveProject")]
        public async Task<IActionResult> LeaveProject([FromBody]ProjectUserRequest req)
        {
            var dto = new ProjectUserDto
            {
                EmployeeId = req.EmployeeId,
                ProjectId = req.ProjectId
            };

            await projectService.LeaveProjectAsync(dto);

            return Ok();
        }

        [HttpPost("api/project/{id}/addMembers")]
        public async Task<IActionResult> AddMembers([FromRoute]int id,[FromBody]ProjectUserRequest req)
        {
            var result =await projectService.AddProjectMembersAsync(id, req);

            return Ok(result);
        }

        [HttpPatch("api/projectColumn/updateTitle/{id}")]
        public async Task<IActionResult> UpdateColumnTitle([FromRoute]int id, [FromBody]ProjectColumnDto req)
        {
            await projectService.UpdateProjectColumnTitleAsync(id, req);

            return Ok();
        }

        [HttpPatch("api/project/update/{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute]int id, [FromBody]ProjectApiDto req)
        {
            req.DateUpdated = DateTime.UtcNow;
            await projectService.UpdateProjectAsync(id, req.ToProjectApiModel<ProjectDto>());

            return Ok();
        }

        [HttpPost("api/projectColumn/updateColumn/{id}")]
        public async Task<IActionResult> UpdateColumnOrder([FromRoute]int id, [FromBody]ProjectColumnDto req)
        {
            await projectService.UpdateProjectColumnOrderAsync(id, req);

            return Ok();
        }
    }
}
