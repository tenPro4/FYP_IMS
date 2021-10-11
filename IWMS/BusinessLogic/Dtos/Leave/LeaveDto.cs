using BusinessLogic.Dtos.Employee;
using BusinessLogicShared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Leave
{
    public class LeaveDto
    {
        public LeaveDto()
        {
            Approved = false;
            Upload = null;
        }

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public string RequestComments { get; set; }
        public bool? Approved { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDto Employee { get; set; }

        public int DepartmentId { get; set; }

        public LeaveType LeaveType { get; set; }

        public SupportFileDto SupportFile { get; set; }
        public int? SupportFileId { get; set; }

        public IFormFile Upload { get; set; }

        public (byte[],string,string) FileDownloadLink { get; set; }

    }
}
