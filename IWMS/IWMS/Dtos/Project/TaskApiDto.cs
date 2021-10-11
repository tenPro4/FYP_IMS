using IWMS.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Project
{
    public class TaskApiDto
    {
        public TaskApiDto()
        {
            Assignees = new List<int>();
            TaskComment = new List<TaskCommentApiDto>();
        }

        public int TaskId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int ListPosition { get; set; }

        public string TaskType { get; set; }

        public int TaskPriority { get; set; }

        public int? CreatedByUserId { get; set; }

        public int ColumnId { get; set; }

        public ICollection<int> Assignees { get; set; }
        //public AccountDto AssignedToEmployee { get; set; }

        public ICollection<TaskCommentApiDto> TaskComment { get; set; }
    }
}
