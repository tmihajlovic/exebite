using Exebite.GoogleSheetAPI.Services;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private readonly IGoogleSheetAPIService _googleSheetApiService;

        public GoogleApiImport(IGoogleSheetAPIService googleSheetApiService)
        {
            _googleSheetApiService = googleSheetApiService;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestaurantMenu()
        {
            _googleSheetApiService.UpdateDailyMenuLipa();
            _googleSheetApiService.UpdateDailyMenuMimas();
        }
    }
}
