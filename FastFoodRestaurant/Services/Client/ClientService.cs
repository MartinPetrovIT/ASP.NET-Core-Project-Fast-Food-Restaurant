using AutoMapper;
using AutoMapper.QueryableExtensions;
using FastFoodRestaurant.Models.Client;
using FastFoodResturant.Data;
using System.Linq;

namespace FastFoodRestaurant.Services.Client
{
    public class ClientService : IClientService
    {

        public ClientService(ApplicationDbContext data, IMapper mapper)
        {
            this.data = data;

            this.mapper = mapper;
        }
        private readonly ApplicationDbContext data;
        private readonly IMapper mapper;


        public InformationModel ShowInformation(string userId)
        =>
            data.Clients.Where(x => x.Id == userId)
            .ProjectTo<InformationModel>(mapper.ConfigurationProvider)
           .FirstOrDefault();

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
