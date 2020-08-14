﻿using Exebite.GoogleSheetAPI.Connectors.Kasa;
using Exebite.GoogleSheetAPI.Connectors.Restaurants;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Services;
using Exebite.GoogleSheetAPI.SheetExtractor;
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
                .AddTransient<IGoogleSheetExtractor, GoogleSheetExtractor>()
                .AddTransient<IMimasConnector, MimasConnector>()
                .AddTransient<ILipaConnector, LipaConnector>()
                .AddTransient<ITopliObrokConnector, TopliObrokConnector>()
                .AddTransient<IIndexConnector, IndexConnector>()
                .AddTransient<IHeyDayConnector, HeyDayConnector>()
                .AddTransient<IParrillaConnector, ParrillaConnector>()
                .AddTransient<ISerpicaConnector, SerpicaConnector>()
                .AddTransient<IKasaConnector, KasaConnector>()
                .AddTransient<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>()
                .AddTransient<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>()
                .AddTransient<IGoogleSheetAPIService, GoogleSheetAPIService>()
                .AddTransient<IGoogleSheetDataAccessService, GoogleSheetDataAccessService>();
        }
    }
}