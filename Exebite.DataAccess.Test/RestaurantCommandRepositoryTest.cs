using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.Mocks;
using Microsoft.Data.Sqlite;
using Optional.Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class RestaurantCommandRepositoryTest : CommandRepositoryTests<RestaurantCommandRepositoryTest.Data, int, RestaurantInsertModel, RestaurantUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          Name = "Restaurant name" + content,
                          DailyMenuId = content
                      });

        protected override IDatabaseCommandRepository<int, RestaurantInsertModel, RestaurantUpdateModel> CreateSut(SqliteConnection connection)
        {
            return CreateOnlyRestaurantCommandRepositoryInstanceNoData(connection);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(SqliteConnection connection, int count)
        {
            using (var context = new InMemoryDBFactory(connection).Create())
            {
                var locations = Enumerable.Range(1, count).Select(x => new RestaurantEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    DailyMenu = new DailyMenuEntity()
                    {
                        Id = x
                    }
                });

                context.Restaurants.AddRange(locations);
                context.SaveChanges();
            }
        }

        protected override RestaurantInsertModel ConvertToInput(Data data)
        {
            return new RestaurantInsertModel { Name = data.Name, DailyMenuId = data.DailyMenuId };
        }

        protected override RestaurantUpdateModel ConvertToUpdate(Data data)
        {
            return new RestaurantUpdateModel { Name = data.Name, DailyMenuId = data.DailyMenuId };
        }

        public sealed class Data
        {
            public string Name { get; set; }

            public int DailyMenuId { get; set; }
        }
    }
}
