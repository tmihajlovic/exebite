using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Services
{
    /// <summary>
    /// Service that manages access to all of the restaurant/kasa connectors.
    /// </summary>
    public interface IGoogleSheetAPIService
    {
        /// <summary>
        /// Update all Customers in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateCustomers();

        /// <summary>
        /// Update daily menu for Lipa restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateDailyMenuLipa();

        /// <summary>
        /// Update daily menu for Topli obrok restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateDailyMenuTopliObrok();

        /// <summary>
        /// Update daily menu for Mimas restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateDailyMenuMimas();

        /// <summary>
        /// Update daily menu for Serpica restaurant in the db based on data from the API.
        /// </summary>
        void UpdateDailyMenuSerpica();

        /// <summary>
        /// Update daily menu for Parrilla restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateDailyMenuParrilla();

        /// <summary>
        /// Update main menu for Parrilla restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateMainMenuParrilla();

        /// <summary>
        /// Update main menu for Index restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateMainMenuIndex();

        /// <summary>
        /// Update main menu for Hey Day restaurant in the db based on data from the spreadsheets.
        /// </summary>
        void UpdateMainMenuHeyDay();

        void WriteOrder(string customerName, string locationName, ICollection<Meal> meals);
    }
}
