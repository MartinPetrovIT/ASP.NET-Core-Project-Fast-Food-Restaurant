namespace FastFoodRestaurant.Models.Drink
{
    public class DrinkListingModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        
        public string ImageUrl { get; set; }

        
        public decimal Price { get; set; }

        public bool IsAlcoholic { get; init; }

        public int ItemId { get; set; }

    }
}
