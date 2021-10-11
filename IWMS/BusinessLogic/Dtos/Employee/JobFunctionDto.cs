using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class JobFunctionDto
    {
        public int JobFunctionId { get; set; }
        public int SectionId { get; set; }
        public string FunctionName { get; set; }
        public string FunctionCode { get; set; }
        public SectionDto Section { get; set; }
    }
}
