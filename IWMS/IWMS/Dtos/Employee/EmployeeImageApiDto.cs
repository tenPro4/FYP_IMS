using Microsoft.AspNetCore.Http;

namespace IWMS.Dtos.Employee
{
    public class EmployeeImageApiDto
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public int EmployeeId { get; set; }
        public string ImageHeader { get; set; }

        public byte[] ImageBinary { get; set; }
        public IFormFile EmployeeImage { get; set; }
    }
}
