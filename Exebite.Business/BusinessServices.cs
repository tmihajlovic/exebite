using Exebite.Business.GoogleApiImportExport;
using Exebite.GoogleSheetAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Business
{
    /// <summary>
    /// Exebite.Business Module services.
    /// </summary>
    public static class BusinessServices
    {
        /// <summary>
        /// Add ALL business services.
        /// </summary>
        /// <param name="services">Collection of existing services.</param>
        /// <returns>All business services.</returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services.AddGoogleSheetsServices()
                           .AddTransient<IMenuService, MenuService>()
                           .AddTransient<IRoleService, RoleService>();
        }

        /// <summary>
        /// Add GoogleSheets-related services.
        /// </summary>
        /// <param name="services">Collection of existing services.</param>
        /// <returns>GoogleSheets-related services.</returns>
        public static IServiceCollection AddGoogleSheetsServices(this IServiceCollection services)
        {
            return services.AddTransient<IGoogleDataExporter, GoogleApiExport>()
                           .AddTransient<IGoogleDataImporter, GoogleApiImport>()
                           .AddTransient<IGoogleSheetAPIService, GoogleSheetAPIService>();
        }
    }
}