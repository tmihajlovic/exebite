namespace Exebite.DataAccess.Repositories
{
    public class RestaurantQueryModel : QueryBase
    {
        public RestaurantQueryModel()
        {
        }

        public RestaurantQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public long? Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
