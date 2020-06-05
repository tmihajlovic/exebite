using System.Collections.Generic;
using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.Business
{
    public interface IMenuService
    {
        /// <summary>
        /// Get restaurants with daily menu
        /// </summary>
        /// <returns>List of restaurants</returns>
        Either<Error, PagingResult<Restaurant>> GetRestorantsWithMenus();

        /// <summary>
        /// Checks for special offers or food bundels that affect price
        /// </summary>
        /// <param name="meal"><see cref="Meal"/> to check offers for</param>
        /// <returns>Price of meal</returns>
        decimal CheckPrice(Meal meal);

        /// <summary>
        /// Find if <see cref="Meal"/> has sidedishes, condament and salads that pair with it
        /// </summary>
        /// <param name="mealId">Id of meal</param>
        /// <returns>List of side dishes, condament and salads that pair with given meal</returns>
        IList<Meal> GetCondimentsForMeal(long mealId);
    }
}
