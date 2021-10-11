using BusinessLogic.Dtos.Employee;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class ImageWriter : IImageWriter
    {
        public async Task<EmployeeImageDto> UploadImage(IFormFile file)
        {
            if (!CheckImageFile(file))
            {
                //throw exception
            }

            return await WriteFile(file);
        }

        private async Task<EmployeeImageDto> WriteFile(IFormFile file)
        {
            //string fileName;
            //try
            //{
            //    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            //    //fileName = Guid.NewGuid().ToString() + extension;
            //    fileName = Path.GetRandomFileName() + extension;
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
            //    //var path = Path.Combine(Directory.GetCurrentDirectory(), "clientapp/public/assets/img/avatars", fileName);

            //    using (var bits = new FileStream(path, FileMode.Create))
            //    {
            //        await file.CopyToAsync(bits);
            //    }
            //}
            //catch (Exception e)
            //{
            //    return e.Message;
            //}

            //return fileName;
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            //fileName = Guid.NewGuid().ToString() + extension;
            string fileName = Path.GetRandomFileName() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var image = new EmployeeImageDto()
                {
                    ImageName = fileName,
                    ImageHeader = "data:" + file.ContentType + ";base64,",
                    ImageBinary = memoryStream.ToArray()
                };

                return image;
            }
        }

        public void DeleteImage(string fileName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool CheckImageFile(IFormFile file)
        {
            byte[] filterBytes;
            using(var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                filterBytes = ms.ToArray();
            }

            return ImageHelper.GetImageFormat(filterBytes) != ImageHelper.ImageFormat.unknown;
        }
    }
}
