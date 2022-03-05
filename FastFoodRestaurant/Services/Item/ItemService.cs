using FastFoodResturant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastFoodRestaurant.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext data;
        public ItemService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public int Add(string name, decimal price)
        {
            var item = new FastFoodRestaurant.Data.Models.Item()
            {

                Name = name,
                Price =price,
               

            };
            data.Items.Add(item);
            data.SaveChanges();

            return item.Id;
        }

        public void Edit(int id, string name, decimal price)
        {
            var item = data.Items.Where(x => x.Id == id).FirstOrDefault();

            item.Name = name;
            item.Price = price;

            data.SaveChanges();      
        }
        
    }
}
