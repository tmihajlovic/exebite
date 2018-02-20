using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IFoodHandler : IDatabaseHandler<Food>
    {
        // Add functions specific for IFoodHandler
        IEnumerable<Food> GetByRestaurant(Restaurant restaurant);
    }
}
