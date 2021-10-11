using EntityFramework.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.Entities
{
    public partial class Employee : IBaseEntity
    {
        public Employee()
        {
            Project = new HashSet<ProjectUser>();
            Permission = new HashSet<EmployeePermission>();
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

        [ForeignKey(nameof(MasterAccount))]
        public int AccountId { get; set; }
        public MasterAccount MasterAccount { get; set; }

        public EmployeeAddress EmployeeAddress { get; set; }
        public EmployeeImage EmployeeImage { get; set; }
        public EmployeeDepartment Department { get; set; }
        public ICollection<ProjectUser> Project { get; set; }
        public ICollection<EmployeePermission> Permission { get; set; }

    }
}
