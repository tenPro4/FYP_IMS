using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Filters
{
    public class EmployeeFilter
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public string EmployeeDepartment { get; set; }
        public int? SectionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? ShiftId { get; set; }
        public int? PositionId { get; set; }
        public int? FunctionId { get; set; }
        public int? LevelId { get; set; }
        public bool? AvailableFlag { get; set; }
    }
}
