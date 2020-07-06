namespace Exebite.DtoModels
{
    public class RestaurantQueryDto : QueryBaseDto
    {
        public long? Id { get; set; }

        public string Name { get; set; }
    }
}
