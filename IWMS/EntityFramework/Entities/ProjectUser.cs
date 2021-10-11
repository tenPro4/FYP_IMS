using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class ProjectUser:IBaseEntity
    {
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public MasterProject Project { get; set; }
        public int ProjectId { get; set; }
    }
}
