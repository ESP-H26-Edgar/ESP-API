using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESP.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _path = "wwwroot/images";

        public async Task<string> SaveImage(IFormFile file)
        {
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(_path, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }
    }
}
