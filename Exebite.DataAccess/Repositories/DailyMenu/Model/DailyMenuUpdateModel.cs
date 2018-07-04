using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class DailyMenuUpdateModel
    {
        public int RestaurantId { get; set; }

        public List<Food> Foods { get; set; } = new List<Food>();
    }
}