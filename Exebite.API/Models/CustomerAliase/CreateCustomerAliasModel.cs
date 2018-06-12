namespace Exebite.API.Models
{
    public class CreateCustomerAliasModel
    {
        public string Alias { get; set; }

        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }
    }
}
