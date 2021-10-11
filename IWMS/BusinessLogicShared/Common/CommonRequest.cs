using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class CommonRequest
    {
        public List<int> Leaves { get; set; }

        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public List<string> Permission { get; set; }
        public string Role { get; set; }
        public string OldRole { get; set; }
    }
}
