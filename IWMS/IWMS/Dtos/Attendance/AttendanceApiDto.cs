using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Attendance
{
    public class AttendanceApiDto
    {
        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public string WorkDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public decimal WorkDay { get; set; }
        public string WorkShift { get; set; }
        public decimal LateHour { get; set; }
        public int Current { get; set; }
    }
}
