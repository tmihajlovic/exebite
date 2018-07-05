namespace Exebite.API.Models
{
    public class FoodQueryModelDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public int? RestaurantId { get; set; }
    }
}
