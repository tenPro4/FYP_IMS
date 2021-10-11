using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class AttendanceModel
    {
        public int EmployeeId { get; set; }
        public string CardNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string ScanInTime { get; set; }
        public string ScanOutTime { get; set; }
        public string AttendanceDate { get; set; }
        public decimal WorkHours { get; set; }
        public string EmployeeImage { get; set; }
        public int DepartmentId { get; set; }
        public decimal LateHour { get; set; }
    }
}
