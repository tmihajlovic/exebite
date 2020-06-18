using System.Collections.Generic;
using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <summary>
    /// <para>Perform the actual persistence of the <see cref="IGoogleSheetAPIService"/> operations.</para>
    /// </summary>
    public interface IGoogleSheetDataAccessService
    {
        /// <summary>
        /// Update database Customers based on google sheets data.
        /// </summary>
        /// <param name="customers">List of Customers from google sheets.</param>
        /// <returns>Number of added and updated records.</returns>
        Either<Error, (int Added, int Updated)> UpdateCustomers(IEnumerable<Customer> customers);

        /// <summary>
        /// Update database Meals based on google sheets data.
        /// </summary>
        /// <param name="meals">Meals from google sheets.</param>
        /// <returns>Number of added and updated records.</returns>
        Either<Error, (int Added, int Updated)> UpdateDailyMeals(IEnumerable<Meal> meals);

        /// <summary>
        /// Update database Meals based on google sheets data.
        /// </summary>
        /// <param name="meals">Meals from google sheets.</param>
        /// <returns>Number of added and updated records.</returns>
        Either<Error, (int Added, int Updated)> UpdateMeals(IEnumerable<Meal> meals);
    }
}