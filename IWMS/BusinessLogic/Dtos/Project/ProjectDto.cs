using BusinessLogic.Dtos.Employee;
using BusinessLogicShared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class ProjectDto
    {
        public ProjectDto()
        {
            Column = new List<ProjectColumnDto>();
            ProjectUser = new List<ProjectUserDto>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProjectStatus Status { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int EmployeeLeaderId { get; set; }

        public ICollection<ProjectColumnDto> Column { get; set; }
        public ICollection<ProjectUserDto> ProjectUser { get; set; }
    }
}
