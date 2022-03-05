using FastFoodRestaurant.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Client
{
    public interface IClientService
    {

        InformationModel ShowInformation(string userId);

        void SetInformation(string userId, string name, string number, string address);
    }
}
