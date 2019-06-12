using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Kasa;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.GoogleSheetAPI
{
    /// <summary>
    /// Exebite.GoogleSheetAPI Module Services.
    /// </summary>
    public static class GoogleSheetApiServiceProvider
    {
        /// <summary>
        /// Add all GoogleSheets API services.
        /// </summary>
        /// <param name="services">Collection of other existing services.</param>
        /// <returns>GoogleSheets API Services.</returns>
        public static IServiceCollection AddGoogleSheetApiServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IGoogleSheetService, GoogleSheetService>()
                .AddTransient<ITeglasConector, TeglasConector>()
                .AddTransient<ILipaConector, LipaConector>()
                .AddTransient<IHedoneConector, HedoneConector>()
                .AddTransient<IKasaConector, KasaConector>()
                .AddTransient<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>()
                .AddTransient<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>();
        }
    }
}
