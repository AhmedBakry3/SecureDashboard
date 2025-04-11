using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload
         string? Upload(IFormFile file, string FolderName);
        //Delete
         bool Delete(string FilePath);
    }
}
