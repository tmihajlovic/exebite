using System;

namespace Exebite.DataAccess.Repositories
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

        public long? Id { get; set; }

        public long? RestaurantId { get; set; }

        public DateTime? Date { get; set; }
    }
}
