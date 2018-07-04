namespace Exebite.API.Models
{
    public class DailyMenuQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public int? RestaurantId { get; set; }
    }
}