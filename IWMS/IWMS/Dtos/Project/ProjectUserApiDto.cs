using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Project
{
    public class ProjectUserApiDto
    {
        public int ProjectUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeApiDto Employee { get; set; }

        //public int ProjectRoleId { get; set; }
        //public ProjectRoleDto ProjectRole { get; set; }

        public int ProjectId { get; set; }
        public ProjectApiDto Project { get; set; }
    }
}
