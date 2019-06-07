using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Exebite.API.Extensions
{
    public static class NSwagExtension
    {
        /// <summary>
        /// Add NSwag settings in regards to CamelCase (de)serialization of objects.
        /// </summary>
        /// <returns>IMvcBuilder</returns>
        public static IMvcBuilder AddNSwagSettings(this IMvcBuilder builder) =>
            builder.AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
    }
}