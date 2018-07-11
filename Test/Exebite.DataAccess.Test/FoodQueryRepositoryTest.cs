using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Exebite.DomainModel;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class FoodQueryRepositoryTest : QueryRepositoryTests<FoodQueryRepositoryTest.Data, Food, FoodQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}"
            });

        protected override FoodQueryModel ConvertEmptyToQuery()
        {
            return new FoodQueryModel();
        }

        protected override FoodQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override FoodQueryModel ConvertToQuery(Data data)
        {
            return new FoodQueryModel
            {
                Id = data.Id,
            };
        }

        protected override FoodQueryModel ConvertToQuery(int id)
        {
            return new FoodQueryModel { Id = id };
        }

        protected override FoodQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new FoodQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Food, FoodQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyFoodRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Food result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var restaurant = new RestaurantEntity() { Name = "testRestaurant" };

                var insertedRestauran = context.Restaurant.Add(restaurant).Entity;
                var dailyMenu = new DailyMenuEntity()
                {
                    RestaurantId = insertedRestauran.Id
                };

                var foods = Enumerable.Range(1, count).Select(x => new FoodEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    RestaurantId = insertedRestauran.Id
                });

                context.Food.AddRange(foods);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public string Name { get; set; }
        }
    }
}