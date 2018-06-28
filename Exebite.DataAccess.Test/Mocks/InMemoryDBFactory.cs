using Exebite.DataAccess.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Test.Mocks
{
    public sealed class InMemoryDBFactory : IFoodOrderingContextFactory
    {
        private readonly SqliteConnection _connection;

        public InMemoryDBFactory(SqliteConnection connection)
        {
            _connection = connection;
        }

        public FoodOrderingContext Create()
        {
            var options = new DbContextOptionsBuilder<FoodOrderingContext>().UseSqlite(_connection).Options;

            var context = new FoodOrderingContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
