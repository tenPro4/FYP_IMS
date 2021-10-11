using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Department;
using BusinessLogic.Dtos.Permission;
using BusinessLogic.Dtos.Project;
using BusinessLogic.Dtos.Role;
using BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            Project = new List<ProjectUserDto>();
            Permission = new List<EmployeePermissionDto>();
        }

        public int EmployeeId { get; set; }
        public string CardNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime ChangedDate { get; set; }
        public int AccountId { get; set; }

        public EmployeeAddressDto EmployeeAddress { get; set; }
        public EmployeeImageDto EmployeeImage { get; set; }
        public EmployeeDepartmentDto Department { get; set; }
        public string Role { get; set; }
        public ICollection<ProjectUserDto> Project { get; set; }
        public ICollection<EmployeePermissionDto> Permission { get; set; }

        //public EmployeeDepartmentRuleDto EmployeeDepartmentRule { get; set; }

    }
}
