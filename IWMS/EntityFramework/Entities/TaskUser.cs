using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public class TaskUser:IBaseEntity
    {
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public MasterTask Task { get; set; }
    }
}
