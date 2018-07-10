using Microsoft.Extensions.DependencyInjection;

namespace Exebite.Common
{
    public static class CommonServices
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<IEitherMapper, EitherMapper>();
            collection.AddTransient<IGetDateTime, GetDateTime>();
            return collection;
        }
    }
}
