using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Exebite.DataAccess.Context
{
    public class ExebiteDbContextOptionsFactory : IExebiteDbContextOptionsFactory
    {
        private readonly IConfiguration _configRoot;

        public ExebiteDbContextOptionsFactory(IConfiguration configRoot)
        {
            _configRoot = configRoot;
        }

        public DbContextOptions<FoodOrderingContext> Create()
        {
            var connectionString = _configRoot.GetConnectionString("ExeBiteConnectionString");
            var dbContextOptions = new DbContextOptionsBuilder<FoodOrderingContext>().UseSqlServer(connectionString)
                                                                                     .UseLazyLoadingProxies()
                                                                                     .Options;
            return dbContextOptions;
        }
    }
}
