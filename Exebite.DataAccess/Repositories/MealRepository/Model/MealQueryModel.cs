namespace Exebite.DataAccess.Repositories
{
    public class MealQueryModel : QueryBase
    {
        public MealQueryModel()
            : base()
        {
        }

        public MealQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public long? Id { get; set; }

        public long? RestaurantId { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsFromStandardMenu { get; set; }

        public int? Type { get; set; }

    }
}
