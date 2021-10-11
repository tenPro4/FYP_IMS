using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Department
{
    public class DepartmentDto
    {
        public DepartmentDto()
        {
            Employees = new List<EmployeeDepartmentDto>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public List<EmployeeDepartmentDto> Employees { get; set; }

    }
}
