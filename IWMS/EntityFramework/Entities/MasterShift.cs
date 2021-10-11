using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterShift : IBaseEntity
    {
        public MasterShift()
        {
            EmployeeState = new HashSet<EmployeeState>();
            MasterShiftCalendar = new HashSet<MasterShiftCalendar>();
        }

        public byte ShiftId { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<EmployeeState> EmployeeState { get; set; }
        public ICollection<MasterShiftCalendar> MasterShiftCalendar { get; set; }
    }
}
