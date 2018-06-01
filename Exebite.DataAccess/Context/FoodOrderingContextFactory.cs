using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Exebite.DataAccess.Migrations
{
    public class FoodOrderingContextFactory : IFoodOrderingContextFactory, IDesignTimeDbContextFactory<FoodOrderingContext>
    {
        private static string connString = @"Data Source=(local);Initial Catalog=Exebite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private readonly DbContextOptions<FoodOrderingContext> options = new DbContextOptionsBuilder<FoodOrderingContext>().UseSqlServer(connString).UseLazyLoadingProxies().Options;

        public FoodOrderingContext Create()
        {
            return new FoodOrderingContext(options);
        }

        public FoodOrderingContext CreateDbContext(string[] args)
        {
            return new FoodOrderingContext(options);
        }
    }
}
