using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Permission
{
    public class EmployeePermissionDto
    {
        public EmployeeDto Employee { get; set; }
        public int EmployeeId { get; set; }

        public PermissionDto MasterPermission { get; set; }
        public int PermissionId { get; set; }
    }
}
