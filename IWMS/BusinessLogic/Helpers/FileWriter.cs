using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using BusinessLogic.Dtos.Leave;
using System.IO.Compression;
using CSharpLib;

namespace BusinessLogic.Helpers
{
    public class FileWriter : IFileWriter
    {
        public async Task<SupportFileDto> UploadDocument(IFormFile file)
        {
            if (!TextFileHelper.CheckFileFormat(file))
            {
                //throw exception
            }

            return await WriteFile(file);

        }

        private async Task<SupportFileDto> WriteFile(IFormFile file)
        {
            var sf = new SupportFileDto();
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //fileName = Guid.NewGuid().ToString() + extension;
                //string fileName = Path.GetRandomFileName() + extension;
                //var uploads = Path.Combine(_environment.WebRootPath, "upload");  IHostingEnvironment _environment;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", file.FileName);
                //var path = Path.Combine(Directory.GetCurrentDirectory(), "clientapp/public/assets/img/avatars", fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(bits);
                }

                sf.ContentType = extension;
                sf.FileName = file.FileName;
                sf.Size = new FileInfo(path).FormatBytes();
                sf.CreatedAt = DateTime.Now;
            }
            catch (Exception e)
            {
                //return e.Message;
            }

            return sf;
        }

        public (byte[],string, string) DownloadFiles(string fileName)
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", fileName);

            var memoryStream = new MemoryStream();

            //using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            //{
            //    var theFile = archive.CreateEntry(path);
            //    using (var streamWriter = new StreamWriter(theFile.Open()))
            //    {
            //        streamWriter.Write(File.ReadAllText(path));
            //    }
            //}

            //return ("application/zip", memoryStream.ToArray(), zipName);
            byte[] bytes = File.ReadAllBytes(path);
            return (bytes,"application/octet-stream", fileName);
        }

        public void DeleteDocument(string fileName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        //{
        //    var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

        //    var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //        {
        //            files.ForEach(file =>
        //            {
        //                var theFile = archive.CreateEntry(file);
        //                using (var streamWriter = new StreamWriter(theFile.Open()))
        //                {
        //                    streamWriter.Write(File.ReadAllText(file));
        //                }

        //            });
        //        }

        //        return ("application/zip", memoryStream.ToArray(), zipName);
        //    }

        //}
    }
}
