using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class EmployeeState : IBaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int PositionId { get; set; }
        public MasterJobPosition Position { get; set; }

        public int JobFunctionId { get; set; }
        public MasterJobFunction JobFunction { get; set; }

        public byte ShiftId { get; set; }
        public MasterShift Shift { get; set; }

        public int LevelId { get; set; }
        public MasterLevel Level { get; set; }

        public DateTime JoinDate { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
