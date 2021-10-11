using IWMS.Dtos.Account;
using IWMS.Dtos.Department;
using IWMS.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Employee
{
    public class EmployeeApiDto
    {
        public EmployeeApiDto()
        {
            Project = new List<ProjectUserApiDto>();
        }

        public int EmployeeId { get; set; }
        public string CardNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public int AccountId { get; set; }

        public AccountApiDto MasterAccount { get; set; }
        public EmployeeAddressApiDto EmployeeAddress { get; set; }
        public EmployeeImageApiDto EmployeeImage { get; set; }
        public EmployeeDepartmentApiDto Department { get; set; }
        public ICollection<ProjectUserApiDto> Project { get; set; }

    }
}
