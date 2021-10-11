using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework.Entities
{
    public class EmployeeDepartment:IBaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int DepartmentId { get; set; }
        public MasterDepartment MasterDepartment { get; set; }
    }
}
