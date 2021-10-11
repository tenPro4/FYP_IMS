using BusinessLogic.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Project
{
    public class TaskUserDto
    {
        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        public int TaskId { get; set; }
        public TaskDto Task { get; set; }
    }
}
