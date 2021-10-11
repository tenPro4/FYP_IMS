using BusinessLogic.Dtos.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public interface IImageWriter
    {
        Task<EmployeeImageDto> UploadImage(IFormFile file);
        void DeleteImage(string file);
    }
}
