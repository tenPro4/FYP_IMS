using BusinessLogic.Dtos.Leave;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public interface IFileWriter
    {
        Task<SupportFileDto> UploadDocument(IFormFile file);
        void DeleteDocument(string fileName);
        (byte[],string,string) DownloadFiles(string fileName);
    }
}
