using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class ShiftCalendarDto
    {
        public DateTime WorkDate { get; set; }
        public string WorkType { get; set; }
        public byte ShiftId { get; set; }

        public ShiftDto Shift { get; set; }
    }
}
