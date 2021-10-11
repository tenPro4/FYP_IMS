using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Employee
{
    public class EmployeeAddressDto
    {
        public int EmployeeId { get; set; }
        public int EmployeeAddressId { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime ChangedDate { get; set; }
    }
}
