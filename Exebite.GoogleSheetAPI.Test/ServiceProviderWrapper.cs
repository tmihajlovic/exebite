using System;
using Exebite.GoogleSheetAPI;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Test
{
    public static class ServiceProviderWrapper
    {
        public static IServiceProvider GetContainer()
        {
            var serviceProvider = new ServiceCollection()
                                        .AddTransient<IHedoneConector, HedoneConector>()
                                        .AddTransient<ILipaConector, LipaConector>()
                                        .AddTransient<ITeglasConector, TeglasConector>()
                                        .AddTransient<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>()
                                        .AddTransient<IGoogleSheetService, GoogleSheetService>()
                                        .AddTransient<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>()
                                        .BuildServiceProvider();
            return serviceProvider;
        }

        public static T Resolve<T>(this IServiceProvider provider)
        {
            var res = (T)provider.GetService(typeof(T));
            return res;
        }
    }
}
