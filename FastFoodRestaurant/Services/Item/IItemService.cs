using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FastFoodRestaurant.Services.Item
{
    public interface IItemService
    {
        int Add(string name,
            decimal price);

        void Edit(int id,
            string name, 
            decimal price);

        void Delete(int id);

    }
}
