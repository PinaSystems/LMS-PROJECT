using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OAWA.Data.Helpers
{
    public class ImageWriter:IImageWriter
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            if(file==null)
                return null;
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file, FileType.image);
            }
            else if(CheckIfDocFile(file))
            {
                return await WriteFile(file, FileType.doc);
            }
            else if(CheckIfVideoFile(file))
            {
                return await WriteFile(file, FileType.video);
            }
            return "Invalid file";
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            if(file==null)
                return null;
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file, FileType.image);
            }
            return "Invalid image file";
        }

        public async Task<string> UploadDoc(IFormFile file)
        {
            if(file==null)
                return null;
            if (CheckIfDocFile(file))
            {
                return await WriteFile(file, FileType.doc);
            }

            return "Invalid doc file";
        }

        public async Task<string> UploadVideo(IFormFile file)
        {
            if(file==null)
                return null;
            if (CheckIfVideoFile(file))
            {
                return await WriteFile(file, FileType.video);
            }

            return "Invalid video file";
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return ImageWriterHelper.GetImageFormat(fileBytes) != ImageWriterHelper.ImageFormat.unknown;
        }

        public bool CheckIfDocFile(IFormFile file)
        {
           byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return ImageWriterHelper.GetDocFormat(fileBytes)!= ImageWriterHelper.DocFormat.unknown;
        }

        public bool CheckIfVideoFile(IFormFile file)
        {
           byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            var ext= Path.GetExtension(file.FileName).ToLower();
            return ext.Equals(".mp4")||ext.Equals(".mov")||ext.Equals(".mkv")||ext.Equals(".flv");
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> WriteFile(IFormFile file, FileType fileType)
        {
            string fileName= string.Empty;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                // fileName = Guid.NewGuid().ToString() + extension; //Create a new Name 
                //                                               //for the file due to security reasons.
                    fileName=Guid.NewGuid().ToString()+"-"+ DateTime.Now.Ticks + extension;
                    string path = "", subPath="";
                    subPath = "files";
                    // path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles",subPath, fileName);
                    path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles",subPath, fileName);

                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                       await file.CopyToAsync(bits);
                       await bits.FlushAsync();
                       bits.Close();
                       bits.Dispose();
                    }
                return fileName;
            }
            catch (Exception e)
            {
                return fileName;
            }
        }
    }
    public enum FileType
    {
        image=1,
        doc=2,
        video=3
    }
}