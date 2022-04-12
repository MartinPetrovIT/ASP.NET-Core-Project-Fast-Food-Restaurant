using Microsoft.AspNetCore.Http;

namespace FastFoodRestaurant.Services.Image
{
    public interface IImageService
    {

        string Upload(IFormFile img);

        void DeleteImage(string fileName);

        bool? CheckImage(IFormFile image);

    }
}
