using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogicShared.Filters
{
    public class AttendanceFilter : EmployeeFilter
    {
        [Required]
        public string AttendanceDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsLate { get; set; }
    }
}
