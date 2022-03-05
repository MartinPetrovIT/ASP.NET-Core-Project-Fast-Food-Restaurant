using FastFoodRestaurant.Models.Client;
using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Client
{
    public class ClientService : IClientService
    {

        public ClientService(ApplicationDbContext data)
        {
            this.data = data;
        }
        private readonly ApplicationDbContext data;


        public InformationModel ShowInformation(string userId)
        =>
            data.Clients.Where(x => x.Id == userId).Select(x => new InformationModel
            {
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address
            }).FirstOrDefault();

        public void SetInformation(string userId, string name, string number, string address)
        {
            var client = data.Clients.Where(x => x.Id == userId).FirstOrDefault();
            client.Name = name;
            client.Address = address;
            client.PhoneNumber = number;

            data.SaveChanges();
        }

    }
}
