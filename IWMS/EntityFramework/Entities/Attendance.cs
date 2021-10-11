using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class Attendance :IBaseEntity
    {
        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public string WorkDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public int Current { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal WorkDay { get; set; }

        public string WorkShift { get; set; }
        public decimal LateHour { get; set; }
    }
}
