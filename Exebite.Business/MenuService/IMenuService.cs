using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.Business
{
    public interface IMenuService
    {
        /// <summary>
        /// Get restaurants with daily menu
        /// </summary>
        /// <returns>List of restaurants</returns>
        IList<Restaurant> GetRestorantsWithMenus();

        /// <summary>
        /// Checks for special offers or food bundels that affect price
        /// </summary>
        /// <param name="meal"><see cref="Meal"/> to check offers for</param>
        /// <returns>Price of meal</returns>
        decimal CheckPrice(Meal meal);

        /// <summary>
        /// Find if <see cref="Food"/> has sidedishes, condament and salads that pair with it
        /// </summary>
        /// <param name="foodId">Id of food</param>
        /// <returns>List of side dishes, condament and salads that pair with given food</returns>
        List<Food> CheckAvailableSideDishes(int foodId);
    }
}
