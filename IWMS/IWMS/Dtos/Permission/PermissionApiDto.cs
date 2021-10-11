using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Permission
{
    public class PermissionApiDto
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string PermissionCode { get; set; }
    }
}
