using BusinessLogic.Dtos.Attendance;
using BusinessLogic.Helpers;
using BusinessLogicShared.Common;
using BusinessLogicShared.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceDto> GetAttendanceAsync(int empId);

        Task<List<AttendanceModel>> GetActiveAsync(AttendanceFilter filter);

        Task<List<AttendanceModel>> GetAbsentAsync(AttendanceFilter filter);

        Task<AttendanceDto> CheckInAsync(int empId);

        Task<AttendanceDto> CheckOutAsync(int empId);

        Task<bool> UpdateAsync(AttendanceDto attendance);

        Task<bool> ExistsAsync(int empId);

        Task<AttendanceDto> AddAsync(AttendanceDto attendance);
    }
}
