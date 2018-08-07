namespace Exebite.DtoModels
{
    public class CustomerAliasDto
    {
        public int Id { get; set; }

        public string Alias { get; set; }

        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }
    }
}
