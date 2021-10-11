using BusinessLogic.Dtos.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        public int DepartmentId { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }

        public DepartmentDto Department { get; set; }
    }
}
