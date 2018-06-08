namespace Exebite.API.Models
{
    public class UpdateOrderModel
    {
        public int Id { get; set; }

        public int[] FoodIds { get; set; } = new int[0];

        public string Note { get; set; }
    }
}
