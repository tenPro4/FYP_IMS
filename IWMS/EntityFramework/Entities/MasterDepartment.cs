using EntityFramework.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterDepartment : IBaseEntity
    {
        public MasterDepartment()
        {
            Employees = new HashSet<EmployeeDepartment>();
        }

        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public ICollection<EmployeeDepartment> Employees { get; set; }
    }
}
