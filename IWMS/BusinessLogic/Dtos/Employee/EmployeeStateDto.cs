using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class EmployeeStateDto
    {
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }
        public JobPositionDto Position { get; set; }

        public int JobFunctionId { get; set; }
        public JobFunctionDto JobFunction { get; set; }

        public byte ShiftId { get; set; }
        public ShiftDto Shift { get; set; }

        public int LevelId { get; set; }
        public LevelDto Level { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
