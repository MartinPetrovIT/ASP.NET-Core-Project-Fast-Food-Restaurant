using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace FastFoodRestaurant.Services.Image
{
    public class ImageService : IImageService
    {

        public string Upload(IFormFile img)
        {
            string fileName = null;
            if (img != null)
            {
                string uploadDir = "wwwroot/img";
                fileName = Guid.NewGuid().ToString() + "-" + img.FileName;
                string path = $"{uploadDir}/{fileName}";

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    img.CopyTo(fileStream);
                }
                 
            
            }

            return fileName;
        }
        public void DeleteImage(string fileName)
        {
            File.Delete($"wwwroot/img/{fileName}");

        }

        public bool? CheckImage(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            if (image.ContentType.ToLower() != "image/jpg" &&
                    image.ContentType.ToLower() != "image/jpeg" &&
                    image.ContentType.ToLower() != "image/pjpeg" &&
                    image.ContentType.ToLower() != "image/gif" &&
                    image.ContentType.ToLower() != "image/x-png" &&
                    image.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            return true;
        }
    }
}
