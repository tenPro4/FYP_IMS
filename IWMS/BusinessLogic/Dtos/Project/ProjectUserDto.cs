using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class ProjectUserDto
    {
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
