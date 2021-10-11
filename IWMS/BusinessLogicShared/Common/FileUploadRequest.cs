using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public class FileUploadRequest
    {
        public IFormFile Upload { get; set; }
    }
}
