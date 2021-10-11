using BusinessLogic.Dtos.Project;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Helpers
{
    public class SortTaskRequest
    {
        public SortTaskRequest()
        {
            ProjectColumns = new HashSet<ProjectColumnDto>();
            Order = new HashSet<int>();
        }

        public ICollection<ProjectColumnDto> ProjectColumns { get; set; }
        public ICollection<int> Order { get; set; }
    }
}
