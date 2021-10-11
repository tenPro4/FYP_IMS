using BusinessLogic.Dtos.Project;
using BusinessLogic.Services.Interfaces;
using IWMS.Dtos.Project;
using IWMS.Mappers.Project;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskStatusController : Controller
    {
        private readonly IProjectColumnService taskStatusService;

        public TaskStatusController(IProjectColumnService taskStatusService)
        {
            this.taskStatusService = taskStatusService;
        }

        [HttpGet("api/project/status")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await taskStatusService.GetAll());
        }

        [HttpGet("api/project/status/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            return Ok(await taskStatusService.GetById(id));
        }
    }
}
