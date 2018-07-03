namespace Exebite.API.Models
{
    public class RestaurantQueryDto : QueryBaseDto
    {
        public RestaurantQueryDto(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
