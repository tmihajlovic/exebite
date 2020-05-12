using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Exebite.API.Extensions
{
    public static class NSwagExtension
    {
        /// <summary>
        /// Add NSwag settings in regards to CamelCase (de)serialization of objects.
        /// </summary>
        /// <param name="builder">builder</param>
        /// <returns>IMvcBuilder</returns>
        public static IMvcBuilder AddNSwagSettings(this IMvcBuilder builder) =>
            builder.AddNewtonsoftJson(opt =>
                {
                   opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
    }
}