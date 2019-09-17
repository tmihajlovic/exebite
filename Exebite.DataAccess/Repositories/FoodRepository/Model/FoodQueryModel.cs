namespace Exebite.DataAccess.Repositories
{
    public class FoodQueryModel : QueryBase
    {
        public FoodQueryModel()
            : base()
        {
        }

        public FoodQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }

        public int? RestaurantId { get; set; }

        public string Name { get; set; }
    }
}
