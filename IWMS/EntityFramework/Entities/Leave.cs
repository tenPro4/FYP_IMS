using BusinessLogicShared.Constants;
using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class Leave : IBaseEntity
    {
        public Leave()
        {
            Approved = false;
        }

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string RequestComments { get; set; }
        public bool? Approved { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public MasterDepartment MasterDepartment { get; set; }
        public int DepartmentId { get; set; }

        public LeaveType LeaveType { get; set; }

        [ForeignKey(nameof(SupportFileId))]
        public SupportFile SupportFile { get; set; }
        public int? SupportFileId { get; set; }
    }
}
