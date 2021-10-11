using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Request
{
    public class ProjectUserRequest
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public List<int> Members { get; set; }
    }
}
