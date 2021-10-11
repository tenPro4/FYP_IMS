using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Employee;
using BusinessLogicShared.Constants;
using EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class TaskDto
    {
        public TaskDto()
        {
            TaskComment = new List<TaskCommentDto>();
            Assignees = new List<TaskUserDto>();
        }

        public int TaskId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskType TaskType { get; set; }

        public int ColumnId { get; set; }
        //public ProjectColumnDto Column { get; set; }

        public BusinessLogicShared.Constants.TaskPriority TaskPriority { get; set; }
        public int? CreatedByUserId { get; set; }

        public int ListPosition { get; set; }

        public ICollection<TaskUserDto> Assignees { get; set; }
        public ICollection<TaskCommentDto> TaskComment { get; set; }
    }
}
