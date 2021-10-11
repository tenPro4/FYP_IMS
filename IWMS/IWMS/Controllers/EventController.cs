using BusinessLogic.Dtos.Event;
using BusinessLogic.Mapper;
using BusinessLogic.Services.Interfaces;
using IWMS.Dtos.Event;
using IWMS.ExceptionHandling;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventController:Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet("api/event")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await eventService.GetAllAsync());
        }


        [HttpPost("api/event")]
        public async Task<ActionResult> Add([FromBody]EventApiDto dto)
        {
            return Ok(await eventService.AddAsync(dto.ToEventModel<EventDto>()));
        }

        [HttpDelete("api/event/{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id)
        {
            await eventService.DeleteAsync(id);

            return NoContent();
        }
    }
}
