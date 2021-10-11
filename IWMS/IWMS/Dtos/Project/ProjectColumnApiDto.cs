using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Project
{
    public class ProjectColumnApiDto
    {
        public ProjectColumnApiDto()
        {
            Title = "New Column";
            Ids = new HashSet<int>();
            MasterTask = new HashSet<TaskApiDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }

        public int ProjectId { get; set; }

        public ICollection<int> Ids { get; set; }
        public ICollection<TaskApiDto> MasterTask { get; set; }
    }
}
