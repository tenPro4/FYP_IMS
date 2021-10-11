using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class EmployeeImageDto
    {
        public int ImageId { get; set; }
        public int EmployeeId { get; set; }
        public string ImageName { get; set; }
        public string ImageHeader { get; set; }

        public byte[] ImageBinary { get; set; }
        public IFormFile EmployeeImage { get; set; }
    }
}
