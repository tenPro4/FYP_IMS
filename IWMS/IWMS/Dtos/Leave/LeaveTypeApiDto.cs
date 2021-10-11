using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Leave
{
    public class LeaveTypeApiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
