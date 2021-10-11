using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class TaskCommentDto
    {
        public int CommentId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Text { get; set; }

        public int TaskId { get; set; }
        //public TaskDto Task { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
