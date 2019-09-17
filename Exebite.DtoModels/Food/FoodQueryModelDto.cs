namespace Exebite.DtoModels
{
    public class FoodQueryModelDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public int? RestaurantId { get; set; }

        public string Name { get; set; }
    }
}