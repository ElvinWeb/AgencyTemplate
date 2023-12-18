using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Helpers
{
    public class Helper
    {
        public static string GetFileName(string rootPath, string folderName, IFormFile image)
        {
            string fileName = image.FileName.Length > 64 ? image.FileName.Substring(image.FileName.Length - 64, 64) : image.FileName;
            fileName = Guid.NewGuid().ToString() + image.FileName;
            string path = Path.Combine(rootPath, folderName, fileName);

            using (FileStream Stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(Stream);
            }

            return fileName;
        }
    }
}
