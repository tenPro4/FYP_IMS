using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterShiftCalendar : IBaseEntity
    {
        public DateTime WorkDate { get; set; }
        public string WorkType { get; set; }
        public byte ShiftId { get; set; }

        public MasterShift Shift { get; set; }
    }
}
