using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Exebite.DataAccess.Context
{
    /// <summary>
    /// Used only for migrations
    /// </summary>
    public class MigrationsFactory : IDesignTimeDbContextFactory<FoodOrderingContext>
    {
        public FoodOrderingContext CreateDbContext(string[] args)
        {
            var dbContextOptions = new DbContextOptionsBuilder<FoodOrderingContext>()
                .UseSqlServer("Server=(Local);Database=Exebite;Trusted_Connection=True;")
                .UseLazyLoadingProxies().Options;

            return new FoodOrderingContext(dbContextOptions);
        }
    }
}
