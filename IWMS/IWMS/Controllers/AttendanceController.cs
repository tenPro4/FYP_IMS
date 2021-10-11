using BusinessLogic.Dtos.Attendance;
using BusinessLogic.Dtos.Leave;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Common;
using BusinessLogicShared.Constants;
using BusinessLogicShared.Filters;
using IWMS.Dtos.Attendance;
using IWMS.Dtos.Leave;
using IWMS.Helpers;
using IWMS.Mappers.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AttendanceController:Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly ILeaveService _leaveService;

        public AttendanceController(
            IAttendanceService attendanceService,
            ILeaveService leaveService)
        {
            _attendanceService = attendanceService;
            _leaveService = leaveService;
        }

        [HttpPost("api/attendance/getActive")]
        public async Task<IActionResult> GetActive([FromBody]AttendanceFilter filter)
        {
            if(filter.AttendanceDate == null)
            {
                filter.AttendanceDate = DateTime.Today.ToString("yyyy/MM/dd");
            }
            var att = await _attendanceService.GetActiveAsync(filter);

            if (att == null)
                return NoContent();

            return Ok(att);
        }

        [HttpPost("api/attendance/getAbsent")]
        public async Task<IActionResult> GetAbsent([FromBody]AttendanceFilter filter)
        {
             if(filter.AttendanceDate == null)
            {
                filter.AttendanceDate = DateTime.Today.ToString("yyyy/MM/dd");
            }

            var att = await _attendanceService.GetAbsentAsync(filter);

            if (att == null)
                return NoContent();

            return Ok(att);
        }

        [HttpGet("api/attendance/getCheckIn")]
        public async Task<IActionResult> GetCheckIn()
        {
            var empId = Int32.Parse(HttpContext.GetEmpId());

            var result = await _attendanceService.CheckInAsync(empId);

            return Ok(result);
        }

        [HttpGet("api/attendance/getCheckOut")]
        public async Task<IActionResult> GetCheckOut()
        {
            var empId = Int32.Parse(HttpContext.GetEmpId());

            var result =await _attendanceService.CheckOutAsync(empId);

            return Ok(result);
        }

        [HttpGet("api/attendance/getStatus")]
        public async Task<IActionResult> GetCheckInStatus()
        {
            var empId = Int32.Parse(HttpContext.GetEmpId());
            var result = await _attendanceService.GetAttendanceAsync(empId);

            return Ok(result);
        }

        [HttpGet("api/leave/downloadSupportFile/{id}")]
        public async Task<IActionResult> DownloadSupportFile([FromRoute]int id)
        {
            var dto = await _leaveService.GetSingleAsync(id);

            var (bytes, fileType, fileName) = dto.FileDownloadLink;

            var result = File(bytes, fileType, fileName);
            return result;
        }

        [HttpGet("api/leave")]
        public async Task<IActionResult> GetLeaves()
        {
            var dto = await _leaveService.GetLeavesAsync();

            if (dto == null)
                return NoContent();

            return Ok(dto);
        }


        [HttpPost("api/leave/leaveRequest")]
        public async Task<IActionResult> RequestLeave([FromBody]LeaveApiDto dto)
        {
            dto.EmployeeId = Int32.Parse(HttpContext.GetEmpId());

            if (dto != null)
                return Ok(await _leaveService.AddAsync(dto.ToAttendanceApiModel<LeaveDto>()));

            return BadRequest();
        }

        [HttpPost("api/leave/uploadFile/{id}"),DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromRoute]int id, [FromForm]FileUploadRequest file)
        {
            //var dto = new LeaveDto
            //{
            //    Description = "Sick",
            //    LeaveType = (LeaveType)2,
            //    DepartmentId = 1,
            //    EmployeeId = 2,
            //    Start = DateTime.Now,
            //    End = DateTime.Now,
            //    RequestComments = "sick",
            //    Upload = file
            //};

            var result = await _leaveService.UploadFile(id, file.Upload);
            if(result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpDelete("api/leave/removeRequest{id}")]
        public async Task<IActionResult> DeleteLeave([FromRoute]int id)
        {
            if (!await _leaveService.ExistsAsync(id))
                return NotFound();

            await _leaveService.DeleteAsync(id);

            return NoContent();
        }

        [HttpDelete("api/leave/removeLeaves")]
        public async Task<IActionResult> DeleteLeaves([FromBody]CommonRequest req)
        {
            await _leaveService.DeleteLeavesAsync(req);

            return NoContent();
        }

        [HttpGet("api/leave/{id}")]
        public async Task<IActionResult> GetLeave([FromRoute]int id)
        {
            var dto = await _leaveService.GetSingleAsync(id);

            if (dto != null)
                return Ok(dto);

            return NotFound();
        }

        [HttpPatch("api/leave/updateRequest/{id}")]
        public async Task<IActionResult> UpdateLeave([FromRoute]int id,[FromBody]LeaveApiDto dto)
        {
            if (!await _leaveService.ExistsAsync(id))
                return NotFound();

            return Ok(await _leaveService.UpdateAsync(id, dto.ToAttendanceApiModel<LeaveDto>()));
        }

        [HttpPut("api/leave/leaveApproval/{id}")]
        public async Task<IActionResult> ApproveLeave([FromRoute]int id)
        {
            if (!await _leaveService.ExistsAsync(id))
                return NotFound();

            return Ok(await _leaveService.LeaveApproval(id));
        }

        [HttpGet("api/leave/unapprove")]
        public async Task<IActionResult> GetUnApproveLeaves()
        {
            var dto = await _leaveService.GetUnApproveLeaveAsync();

            if (dto != null)
                return Ok(dto);

            return NoContent();
        }
    }
}
