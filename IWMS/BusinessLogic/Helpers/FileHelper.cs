using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Helpers
{
    public class TextFileHelper
    {
        public static bool CheckFileFormat(IFormFile file)
        {
            var supportedTypes = new[] { "txt", "doc", "docx", "pdf" };
            var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
            if (!supportedTypes.Contains(fileExt))
            {
                return false;
            }

            return true;
        }
    }
}
