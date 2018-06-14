using Exebite.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Test.InMemoryDB
{
    public class InMemoryDBFactory : IFoodOrderingContextFactory
    {
        private readonly DbContextOptions<FoodOrderingContext> options = new DbContextOptionsBuilder<FoodOrderingContext>().UseInMemoryDatabase("TestDB").UseLazyLoadingProxies(true).Options;

        public FoodOrderingContext Create()
        {
            return new FoodOrderingContext(options);
        }
    }
}
