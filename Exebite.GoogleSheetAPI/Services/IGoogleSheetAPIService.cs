﻿namespace Exebite.GoogleSheetAPI.Services
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
    }
}