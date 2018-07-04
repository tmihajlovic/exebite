namespace Exebite.API.Models
{
    public class RestaurantQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
