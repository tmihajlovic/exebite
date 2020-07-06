using System;
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

        public DbContextOptions<MealOrderingContext> Create()
        {
            var environmentConnectionString = Environment.GetEnvironmentVariable("ExeBiteConnectionString", EnvironmentVariableTarget.User);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                                    ? environmentConnectionString
                                    : _configRoot.GetConnectionString("ExeBiteConnectionString");

            var dbContextOptions = new DbContextOptionsBuilder<MealOrderingContext>().UseSqlServer(connectionString)
                                                                                     .UseLazyLoadingProxies()
                                                                                     .Options;
            return dbContextOptions;
        }
    }
}
