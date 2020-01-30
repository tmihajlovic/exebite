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
        /// Update database Foods based on google sheets data.
        /// </summary>
        /// <param name="foods">Food from google sheets.</param>
        /// <returns>Number of added and updated records.</returns>
        Either<Error, (int Added, int Updated)> UpdateFoods(IEnumerable<Food> foods);
    }
}