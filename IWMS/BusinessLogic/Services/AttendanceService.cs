using BusinessLogic.Services.Interfaces;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Dtos.Attendance;
using BusinessLogic.Helpers;
using System.Threading.Tasks;
using System.Linq;
using EntityFramework.Specifications;
using BusinessLogic.Mapper;
using BusinessLogicShared.Filters;
using BusinessLogicShared.Common;
using System.Globalization;

namespace BusinessLogic.Services
{
    public class AttendanceService :IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IAsyncRepository<Attendance> asyncRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository,
            IAsyncRepository<Attendance> asyncRepository)
        {
            _attendanceRepository = attendanceRepository;
            this.asyncRepository = asyncRepository;
        }

        public async Task<AttendanceDto> AddAsync(AttendanceDto attendance)
        {
            var att = await asyncRepository.AddAsync(attendance.ToAttendanceModel<Attendance>());
            return att.ToAttendanceModel<AttendanceDto>();
        }

        public async Task<AttendanceDto> CheckInAsync(int empId)
        {
            if (await ExistsAsync(empId))
            {
                var latestRecord = _attendanceRepository.GetLastestRecord(empId);
                var getRecord = await asyncRepository.GetSingleWithNoTrackAsync(x => x.Id == latestRecord);

                if (getRecord.WorkDate == DateTime.Today.ToString("yyyy/MM/dd")) {
                    getRecord.Current = 1;
                    getRecord.TimeIn = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
                    await UpdateAsync(getRecord.ToAttendanceModel<AttendanceDto>());

                    return getRecord.ToAttendanceModel<AttendanceDto>();
                }
                else
                {
                    var newRecord = new AttendanceDto
                    {
                        TimeIn = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"),
                        WorkDate = DateTime.Today.ToString("yyyy/MM/dd"),
                        WorkDay = 0,
                        TimeOut = "",
                        WorkShift = "n",
                        EmployeeId = empId,
                        Current = 1
                    };

                    if(DateTime.Now.Hour > 10)
                    {
                        TimeSpan workingHour = TimeSpan.FromHours(10);
                        var currentime = DateTime.Now.TimeOfDay;

                        var lateHours = currentime.Subtract(workingHour);
                        newRecord.LateHour = Math.Round(Convert.ToDecimal(lateHours.TotalHours), 2);
                    }

                    await AddAsync(newRecord);

                    return newRecord;
                }
            }
            else
            {
                //first time check in
                var newAttendance = new AttendanceDto
                {
                    EmployeeId = empId,
                    WorkDate = DateTime.Today.ToString("yyyy/MM/dd"),
                    TimeIn = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss"),
                    TimeOut = "",
                    WorkDay = 0,
                    WorkShift = "n",
                    Current = 1,
                };

                if (DateTime.Now.Hour > 10)
                {
                    TimeSpan workingHour = TimeSpan.FromHours(10);
                    var currentime = DateTime.Now.TimeOfDay;

                    var lateHours = currentime.Subtract(workingHour);
                    newAttendance.LateHour = Math.Round(Convert.ToDecimal(lateHours.TotalHours), 2);
                }

                await AddAsync(newAttendance);

                return newAttendance;
            };
        }

        public async Task<AttendanceDto> CheckOutAsync(int id)
        {
            var att = await asyncRepository.GetSingleWithNoTrackAsync(
                x => x.EmployeeId == id && x.WorkDate == DateTime.Today.ToString("yyyy/MM/dd"));

            att.TimeOut = DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss");
            att.Current = 0;

            var currentime = DateTime.Now.TimeOfDay;
            TimeSpan startTime;
            if(TimeSpan.TryParse(att.TimeIn,out startTime))
            {
                var hoursWork = currentime.Subtract(startTime);
                att.WorkDay += Math.Round(Convert.ToDecimal(hoursWork.TotalHours),2);
            }
            await UpdateAsync(att.ToAttendanceModel<AttendanceDto>());

            return att.ToAttendanceModel<AttendanceDto>();
        }

        public async Task<bool> ExistsAsync(int empId)
        {
            return await asyncRepository.ExistsAsync(x => x.EmployeeId == empId);
        }

        public async Task<List<AttendanceModel>> GetAbsentAsync(AttendanceFilter filter)
        {
            return await _attendanceRepository.GetAbsentAsync(filter);
        }

        public async Task<List<AttendanceModel>> GetActiveAsync(AttendanceFilter filter)
        {
            return await _attendanceRepository.GetActiveAsync(filter);
        }

        public async Task<AttendanceDto> GetAttendanceAsync(int empId)
        {
            var latestRecord = _attendanceRepository.GetLastestRecord(empId);
            var getRecord = await asyncRepository.GetSingleWithNoTrackAsync(x => x.Id == latestRecord);

            if (getRecord.WorkDate == DateTime.Today.ToString("yyyy/MM/dd"))
                return getRecord.ToAttendanceModel<AttendanceDto>();

            return null;
        }

        public async Task<bool> UpdateAsync(AttendanceDto attendance)
        {
            return await asyncRepository.UpdateAsync(attendance.ToAttendanceModel<Attendance>());
        }
    }
}
