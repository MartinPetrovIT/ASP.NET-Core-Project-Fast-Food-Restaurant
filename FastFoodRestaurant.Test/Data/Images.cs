using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Test.Data
{
    public  class Images
    {
        public static  IFormFile Image()
        {
            var content = "Fake image";
            var fileName = "test.jpeg";
            var contentType = "image/jpeg";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };


            return file;
        }
    }
}
