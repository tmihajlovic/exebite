using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
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
                      });

        protected override IDatabaseCommandRepository<int, RestaurantInsertModel, RestaurantUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyRestaurantCommandRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count).Select(x => new RestaurantEntity()
                {
                    Id = x,
                    Name = $"Name {x}"
                });

                context.Restaurants.AddRange(locations);
                context.SaveChanges();
            }
        }

        protected override RestaurantInsertModel ConvertToInput(Data data)
        {
            return new RestaurantInsertModel { Name = data.Name };
        }

        protected override RestaurantUpdateModel ConvertToUpdate(Data data)
        {
            return new RestaurantUpdateModel { Name = data.Name };
        }

        protected override RestaurantInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RestaurantUpdateModel ConvertToInvalidUpdate(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override int GetUnExistingId()
        {
            return 99999;
        }

        public sealed class Data
        {
            public string Name { get; set; }
        }
    }
}
