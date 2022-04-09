namespace FastFoodRestaurant
{
    public class WebConstants
    {

        public const string GlobalWarningMessageKey = "GlobalWarningMessage"; 

        public const string GlobalMessageKey = "GlobalMessage";

        public const string MessageForFormats = "Invalid Image Format! The valid formats are: jpg, jpeg, png, gif";

        public const string MessageForNullImageAndFormats = "You should add image! The valid formats are: jpg, jpeg, png, gif";

        public const string MessageForNotExistingCategory = "Category does not exist!";

        public const string InvalidDate = "Invalid date!";
        
        public const string OrderInformationSholdbeaddedBeforeMakeAnOrder = "You should add order information, before make an order!";

        public const string ItemIsAdded = "The item is added to your cart!";

        public const string ClickRemoveButton = "If you want to remove item click remove button!";

        public const string CompletedOrder = "Your order is completed!";

        public const string OrderPriceAndAllFeildsMustBeFilled = "Order price can not be 0.00 and all information fields must be filled!";
       
        
        
        public class Cache
        {
            public const string LatestFoodsCacheKey = nameof(LatestFoodsCacheKey);
        }
    }
}
