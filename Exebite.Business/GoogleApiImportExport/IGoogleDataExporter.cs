using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business.GoogleApiImportExport
{
    public interface IGoogleDataExporter
    {
        /// <summary>
        /// Place orders in sheet of restaurant
        /// </summary>
        /// <param name="orderList">Orders to place</param>
        /// <param name="restaurant">Restaurant to place orders to</param>
        void PlaceOrders(List<Order> orderList, Restaurant restaurant);

        /// <summary>
        /// Order Daily menu sheet so first column is today and place corect dates
        /// </summary>
        void SetupDailyMenuDayOrder();

        /// <summary>
        /// TBE
        /// </summary>
        void UpdateKasaTab();
    }
}
