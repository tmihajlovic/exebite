using Exebite.DataAccess.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Exebite.DataAccess.Test.Mocks
{
    public sealed class InMemoryDBFactory : IMealOrderingContextFactory
    {
        private readonly SqliteConnection _connection;

        public InMemoryDBFactory(SqliteConnection connection)
        {
            _connection = connection;
        }

        public MealOrderingContext Create()
        {
            var options = new DbContextOptionsBuilder<MealOrderingContext>().UseSqlite(_connection).Options;

            var context = new MealOrderingContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}
