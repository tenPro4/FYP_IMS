using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class ProjectColumnDto
    {
        public ProjectColumnDto()
        {
            Title = "New Column";
            Ids = new HashSet<int>();
            MasterTask = new HashSet<TaskDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public int ProjectId { get; set; }
        //public ProjectDto MasterProject { get; set; }

        public ICollection<int> Ids { get; set; }

        public ICollection<TaskDto> MasterTask { get; set; }
    }
}
