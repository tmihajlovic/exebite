using Exebite.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Test.Mocks
{
    public sealed class InMemoryDBFactory : IFoodOrderingContextFactory
    {

        private readonly string _name;

        public InMemoryDBFactory(string name)
        {
            _name = name;
        }

        public FoodOrderingContext Create()
        {
            var options = new DbContextOptionsBuilder<FoodOrderingContext>()
                .UseInMemoryDatabase(_name)
                .UseLazyLoadingProxies(true)
                .Options;
            return new FoodOrderingContext(options);
        }
    }
}
