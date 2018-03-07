using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IFoodRepository : IDatabaseRepository<Food>
    {
        IEnumerable<Food> GetByRestaurant(Restaurant restaurant);
    }
}
