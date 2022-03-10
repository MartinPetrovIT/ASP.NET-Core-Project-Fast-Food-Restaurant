namespace FastFoodRestaurant.Models.Item
{
    public class ItemListingModel
    {
        public int Id { get; init; }

        public string Name  { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
