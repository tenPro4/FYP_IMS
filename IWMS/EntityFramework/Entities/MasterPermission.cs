using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterPermission:IBaseEntity
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCode { get; set; }
    }
}
