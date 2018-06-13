namespace Exebite.Business.GoogleApiImportExport
{
    public interface IGoogleDataExporter
    {
        /// <summary>
        /// Place orders in sheet of restaurant
        /// </summary>
        /// <param name="restaurantName">Name of restaurant to place orders</param>
        void PlaceOrdersForRestaurant(string restaurantName);

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
