using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public class EmployeePermission: IBaseEntity
    {
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("PermissionId")]
        public MasterPermission MasterPermission { get; set; }
        public int PermissionId { get; set; }

    }
}
