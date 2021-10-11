using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Request
{
    public class AddProjectRequest
    {
        public AddProjectRequest()
        {
            ProjectUser = new List<int>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public int EmployeeLeaderId { get; set; }

        public List<int> ProjectUser { get; set; }
    }
}
