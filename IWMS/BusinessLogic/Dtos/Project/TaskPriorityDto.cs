using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class TaskPriorityDto
    {
        public TaskPriorityDto()
        {
            Task = new List<TaskDto>();
        }

        public int PriorityId { get; set; }
        public string PriorityCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }

        public ICollection<TaskDto> Task { get; set; }
    }
}
