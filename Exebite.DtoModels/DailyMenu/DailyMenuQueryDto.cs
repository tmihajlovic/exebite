namespace Exebite.DtoModels
{
    public class DailyMenuQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public int? RestaurantId { get; set; }
    }
}