namespace Exebite.DomainModel
{
    public class DailyMenuQueryModel : QueryBase
    {
        public DailyMenuQueryModel()
        {
        }

        public DailyMenuQueryModel(int page, int size)
            : base(page, size)
        {
        }
        public int? Id { get; set; }

        public int? RestaurantId { get; set; }
    }
}
