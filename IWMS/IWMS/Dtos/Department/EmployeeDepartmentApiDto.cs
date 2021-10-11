using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Department
{
    public class EmployeeDepartmentApiDto
    {
        public int EmployeeId { get; set; }
        public EmployeeApiDto Employee { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentApiDto Department { get; set; }
    }
}
