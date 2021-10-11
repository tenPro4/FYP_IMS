using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Department
{
    public class DepartmentApiDto
    {
        public DepartmentApiDto()
        {
            Employees = new List<EmployeeDepartmentApiDto>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public List<EmployeeDepartmentApiDto> Employees { get; set; }
    }
}
