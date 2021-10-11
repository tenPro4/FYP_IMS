using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class ShiftDto
    {
        public byte ShiftId { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
