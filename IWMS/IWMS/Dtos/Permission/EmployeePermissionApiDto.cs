using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Permission
{
    public class EmployeePermissionApiDto
    {
        public EmployeeApiDto Employee { get; set; }
        public int EmployeeId { get; set; }

        public PermissionApiDto MasterPermission { get; set; }
        public int PermissionId { get; set; }
    }
}
