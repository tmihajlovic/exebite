namespace Exebite.API.Models
{
    public class UpdateCustomerAliasModel
    {
        public string Alias { get; set; }

        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }
    }
}
