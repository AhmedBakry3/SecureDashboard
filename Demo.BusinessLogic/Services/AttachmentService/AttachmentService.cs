using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtension = [".png", ".jpg", ".jpeg"];
        const int MaxSize = 2_097_152;
        public string? Upload(IFormFile file, string FolderName)
        {
            if (file is null || file.Length == 0) return null;
            //1.Check Extension
            var Extension = Path.GetExtension(file.FileName); //.png
            if (!AllowedExtension.Contains(Extension)) return null;

            //2.Check Size
            if(file.Length == 0 || file.Length > MaxSize) return null;

            //3.Get Located Folder Path
            //var FolderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{FolderName}";

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files" , FolderName);

            //4.Make Attachment Name Unique-- GUID
            var FileName = $"{ Guid.NewGuid()}_{file.FileName}";

            //5.Get File Path
            var FilePath = Path.Combine(FolderPath, FileName);

            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream Fs = new FileStream(FilePath, FileMode.Create);

            //7.Use Stream To Copy File
            file.CopyTo(Fs);
            //8.Return FileName To Store In Database
            return FileName;

        }

        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath)) return false;
            else
            {
                File.Delete(FilePath);
                return true;
            }
        }


    }
}
