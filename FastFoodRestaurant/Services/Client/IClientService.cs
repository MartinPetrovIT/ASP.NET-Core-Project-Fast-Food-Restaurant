using FastFoodRestaurant.Models.Client;

namespace FastFoodRestaurant.Services.Client
{
    public interface IClientService
    {

        InformationModel ShowInformation(string userId);

        void SetInformation(string userId, string name, string number, string address);
    }
}
