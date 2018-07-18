using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

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
            var environmentConnectionString = Environment.GetEnvironmentVariable("ExeBiteConnectionString", EnvironmentVariableTarget.User);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                                    ? environmentConnectionString
                                    : _configRoot.GetConnectionString("ExeBiteConnectionString");

            var dbContextOptions = new DbContextOptionsBuilder<FoodOrderingContext>().UseSqlServer(connectionString)
                                                                                     .UseLazyLoadingProxies()
                                                                                     .Options;
            return dbContextOptions;
        }
    }
}
