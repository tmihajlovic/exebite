namespace Exebite.DataAccess.Repositories
{
    public class CustomerAliasInsertModel
    {
        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }

        public string Alias { get; set; }
    }
}