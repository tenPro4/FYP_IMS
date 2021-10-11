using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Department
{
    public class EmployeeDepartmentDto
    {
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        public int DepartmentId { get; set; }
        public DepartmentDto MasterDepartment { get; set; }
    }
}
