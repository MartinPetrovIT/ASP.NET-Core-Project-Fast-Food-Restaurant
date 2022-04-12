using FastFoodRestaurant.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FastFoodRestaurant.Test.Data.TestDataConstants.Client;

namespace FastFoodRestaurant.Test.Data
{
    public class Client
    {
        public static FastFoodRestaurant.Data.Models.Client ValidTestClient = new ()
        {
           Id = TestId,
           Name = TestName,
           Address = TestAddress,
           PhoneNumber = TestPhoneNumber
        };

        public static InformationModel InformationModel
       => new ()
       {
           
           Name = TestName,
           Address = TestAddress,
           PhoneNumber = TestPhoneNumber
   
       };
}
}
