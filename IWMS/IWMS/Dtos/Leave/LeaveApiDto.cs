using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Dtos.Leave
{
    public class LeaveApiDto
    {
        public LeaveApiDto()
        {
            Approved = false;
            Upload = null;
        }

        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Description { get; set; }
        public string RequestComments { get; set; }
        public bool? Approved { get; set; }

        public int EmployeeId { get; set; }

        public int DepartmentId { get; set; }

        public int LeaveType { get; set; }

        public SupportFileApiDto SupportFile { get; set; }
        public int? SupportFileId { get; set; }

        public IFormFile Upload { get; set; }

        public (string, byte[], string) FileDownloadLink { get; set; }
    }
}
