using BusinessLogicShared.Common;
using BusinessLogicShared.Filters;
using EntityFramework.Context;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceModel>> GetAbsentAsync(AttendanceFilter filter)
        {
            var employeeActiveQuery = (from employee in _context.Employee
                                       join attendance in _context.Attendance
                                       on employee.EmployeeId equals attendance.EmployeeId
                                       where attendance.WorkDate == filter.AttendanceDate
                                       select employee.EmployeeId
                                       );

            var employeeListQuery = (from employee in _context.Employee
                                     join account in _context.MasterAccount on employee.AccountId equals account.Id
                                     join ed in _context.EmployeeDepartment on employee.EmployeeId equals ed.EmployeeId
                                     join address in _context.EmployeeAddress on employee.EmployeeId equals address.EmployeeId into add
                                     from address in add.DefaultIfEmpty()
                                     join image in _context.EmployeeImage on employee.EmployeeId equals image.EmployeeId into img
                                     from image in img.DefaultIfEmpty()
                                     select new
                                     {
                                         employee.EmployeeId,
                                         employee.CardNo,
                                         employee.FirstName,
                                         employee.LastName,
                                         account.Email,
                                         ed.DepartmentId,
                                         ed.MasterDepartment.DepartmentCode,
                                         ed.MasterDepartment.DepartmentName,
                                         image.ImageName,
                                         image.ImageHeader,
                                         image.ImageBinary,
                                     });

            //if(filter != null)
            //{
            //    if(filter.DepartmentId.HasValue)
            //        employeeListQuery = employeeListQuery.Where(x => x.DepartmentId == filter.DepartmentId);

            //    if (filter.EmployeeId.HasValue)
            //        employeeListQuery = employeeListQuery.Where(x => x.EmployeeId == filter.EmployeeId);
            //}

            var attendances = await employeeListQuery.Select(x => new AttendanceModel
            {
                EmployeeId = x.EmployeeId,
                CardNo = x.CardNo,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                DepartmentName = x.DepartmentName,
                DepartmentCode = x.DepartmentCode,
                DepartmentId = x.DepartmentId,
                EmployeeImage = x.ImageHeader + x.ImageBinary,
                ScanInTime = "-",
                ScanOutTime = "-",
                AttendanceDate = "-",
                WorkHours = 0,
            }).ToListAsync();

            var employeeActive = await employeeActiveQuery.ToListAsync();
            var employeeList = await employeeListQuery.ToListAsync();

            var employeeAbsent = attendances.Where(x => !employeeActive.Contains(x.EmployeeId)).ToList();

            return employeeAbsent;
        }

        public async Task<List<AttendanceModel>> GetActiveAsync(AttendanceFilter filter)
        {
            var query = (from employee in _context.Employee
                         join account in _context.MasterAccount on employee.AccountId equals account.Id
                         join edp in _context.EmployeeDepartment on employee.EmployeeId equals edp.EmployeeId
                         join address in _context.EmployeeAddress on employee.EmployeeId equals address.EmployeeId into add
                         from address in add.DefaultIfEmpty()
                         join image in _context.EmployeeImage on employee.EmployeeId equals image.EmployeeId into img
                         from image in img.DefaultIfEmpty()
                         join attendance in _context.Attendance on employee.EmployeeId equals attendance.EmployeeId into aa
                         from attendance in aa.DefaultIfEmpty()
                         where attendance.WorkDate == filter.AttendanceDate
                         select new
                         {
                             employee.EmployeeId,
                             employee.CardNo,
                             employee.FirstName,
                             employee.LastName,
                             account.Email,
                             edp.DepartmentId,
                             edp.MasterDepartment.DepartmentCode,
                             edp.MasterDepartment.DepartmentName,
                             //address.City
                             image.ImageName,
                             image.ImageHeader,
                             image.ImageBinary,
                             attendance.WorkDate,
                             attendance.WorkDay,
                             attendance.TimeIn,
                             attendance.TimeOut,
                             attendance.LateHour
                         });

            var attendances = await query.Select(x => new AttendanceModel
            {
                EmployeeId = x.EmployeeId,
                CardNo = x.CardNo,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                DepartmentName = x.DepartmentName,
                DepartmentCode = x.DepartmentCode,
                EmployeeImage = x.ImageHeader+x.ImageBinary,
                AttendanceDate = x.WorkDate,
                ScanInTime = x.TimeIn,
                ScanOutTime = x.TimeOut,
                LateHour = x.LateHour,
                WorkHours = x.WorkDay,
            }).ToListAsync();

            return attendances;
        }

        public async Task<List<AttendanceModel>> GetHistoryAsync(AttendanceFilter filter)
        {
            var query = (from employee in _context.Employee
                         join edr in _context.EmployeeDepartment on employee.EmployeeId equals edr.EmployeeId
                         join address in _context.EmployeeAddress on employee.EmployeeId equals address.EmployeeId
                         join image in _context.EmployeeImage on employee.EmployeeId equals image.EmployeeId into img
                         from image in img.DefaultIfEmpty()
                         join attendance in _context.Attendance on employee.EmployeeId equals attendance.EmployeeId into aa
                         from attendance in aa.DefaultIfEmpty()
                         select new
                         {
                             employee.EmployeeId,
                             employee.CardNo,
                             employee.FirstName,
                             employee.LastName,
                             edr.DepartmentId,
                             edr.MasterDepartment.DepartmentCode,
                             edr.MasterDepartment.DepartmentName,
                             //address.City
                             image.ImageName,
                             image.ImageHeader,
                             image.ImageBinary,
                             attendance.WorkDate,
                             attendance.WorkDay,
                             attendance.TimeIn,
                             attendance.TimeOut,
                             attendance.LateHour
                         });

            if(filter != null)
            {
                if (!string.IsNullOrEmpty(filter.AttendanceDate))
                    query = query.Where(x => x.WorkDate == filter.AttendanceDate);

                if (!string.IsNullOrEmpty(filter.StartDate))
                    query = query.Where(x => Convert.ToDateTime(x.WorkDate) >= Convert.ToDateTime(filter.StartDate));

                if (!string.IsNullOrEmpty(filter.StartDate))
                    query = query.Where(x => Convert.ToDateTime(x.WorkDate) <= Convert.ToDateTime(filter.EndDate));

                if (filter.DepartmentId.HasValue)
                    query = query.Where(x => x.DepartmentId == filter.DepartmentId);

                if(filter.EmployeeId.HasValue)
                    query = query.Where(x => x.EmployeeId == filter.EmployeeId);

                if (filter.IsLate)
                    query = query.Where(x => x.LateHour > 0);
            }

            var attendances = await query.Select(x => new AttendanceModel
            {
                EmployeeId = x.EmployeeId,
                CardNo = x.CardNo,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DepartmentName = x.DepartmentName,
                DepartmentCode = x.DepartmentCode,
                EmployeeImage = x.ImageHeader+x.ImageBinary,
                AttendanceDate = x.WorkDate,
                ScanInTime = x.TimeIn,
                ScanOutTime = x.TimeOut,
                LateHour = x.LateHour,
                WorkHours = x.WorkDay,
            }).ToListAsync();

            return attendances;
        }

        public long GetLastestRecord(int id)
        {
            return _context.Attendance.Where(x => x.EmployeeId == id)
                .AsNoTracking()
                .Max(m => m.Id);
        }
    }
}
