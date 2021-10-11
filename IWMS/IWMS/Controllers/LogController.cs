using BusinessLogic.Dtos.Log;
using BusinessLogic.Services.Interfaces;
using IWMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    [Produces("application/json")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LogController:Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("api/log")]
        public async Task<ActionResult> ErrorsLog(int? page,string search)
        {
            return Ok(await _logService.GetLogsAsync(search, page ?? 1));
        }

        [HttpDelete("api/deletelogs/{date}")]
        public async Task<IActionResult> DeleteLogs([FromRoute]string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            //DateTime oDate = DateTime.Parse(date)

            DateTime oDate = DateTime.ParseExact(date, 
                new string[] { "MM.dd.yyyy", "MM-dd-yyyy", "MM/dd/yyyy" }, 
                provider, DateTimeStyles.None);

            //var sdate = oDate.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);

            var delete = await _logService.DeleteLogsOlderThanAsync(oDate);

            if (delete)
                return Ok();

            return NoContent();
        }

    }
}
