using System;
using AutoMapper;
using Exebite.GoogleSheetAPI;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.Kasa;
using Exebite.GoogleSheetAPI.RestaurantConectors;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Test
{
    public static class ServiceProviderWrapper
    {
        public static IServiceProvider GetContainer()
        {
            //ServiceCollectionExtensions.UseStaticRegistration = false;
            Mapper.Reset();
            var serviceProvider = new ServiceCollection()

                                        .AddTransient<IGoogleSheetService, GoogleSheetService>()
                                        .AddTransient<ITeglasConector, TeglasConector>()
                                        .AddTransient<IRestaurantConector, RestaurantConector>()
                                        .AddTransient<ILipaConector, LipaConector>()
                                        .AddTransient<IHedoneConector, HedoneConector>()
                                        .AddTransient<IKasaConector, KasaConector>()
                                        .AddTransient<IGoogleSpreadsheetIdFactory, GoogleSpreadsheetIdFactory>()
                                        .AddTransient<IGoogleSheetServiceFactory, GoogleSheetServiceFactory>()
                                        .AddAutoMapper()
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
