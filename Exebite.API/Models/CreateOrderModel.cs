namespace Exebite.API.Models
{
    public class CreateOrderModel
    {
        public int[] FoodIds { get; set; } = new int[0];

        public string Note { get; set; }
    }
}
