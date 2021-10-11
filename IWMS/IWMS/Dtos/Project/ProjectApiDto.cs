using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Project
{
    public class ProjectApiDto
    {
        public ProjectApiDto()
        {
            Column = new List<ProjectColumnApiDto>();
            ProjectUser = new List<ProjectUserApiDto>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Status { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int EmployeeLeaderId { get; set; }

        public ICollection<ProjectColumnApiDto> Column { get; set; }
        public ICollection<ProjectUserApiDto> ProjectUser { get; set; }
    }
}
