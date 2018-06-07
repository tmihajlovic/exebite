using Exebite.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Exebite.DataAccess.Migrations
{
    public class FoodOrderingContextFactory : IFoodOrderingContextFactory //, IDesignTimeDbContextFactory<FoodOrderingContext>
    {
        //private static string connString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Exebite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //      private readonly DbContextOptions<FoodOrderingContext> options = new DbContextOptionsBuilder<FoodOrderingContext>().UseSqlServer(Configuration["ConnectionStrings"]).UseLazyLoadingProxies().Options;
        private readonly IExebiteDbContextOptionsFactory _optionsFactory;

        public FoodOrderingContextFactory(IExebiteDbContextOptionsFactory optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public FoodOrderingContext Create()
        {
            var options = _optionsFactory.Create();

            return new FoodOrderingContext(options);
        }

        //public FoodOrderingContext CreateDbContext(string[] args)
        //{
        //    return new FoodOrderingContext(options);
        //}
    }
}
