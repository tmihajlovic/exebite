namespace Exebite.DtoModels
{
    public class MealQueryDto : QueryBaseDto
    {
        public long? Id { get; set; }

        public long? RestaurantId { get; set; }

        public string Name { get; set; }
    }
}