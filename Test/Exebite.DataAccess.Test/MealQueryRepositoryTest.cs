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
    public sealed class MealQueryRepositoryTest : QueryRepositoryTests<MealQueryRepositoryTest.Data, Meal, MealQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content
            });

        protected override MealQueryModel ConvertEmptyToQuery()
        {
            return new MealQueryModel();
        }

        protected override MealQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override MealQueryModel ConvertToQuery(Data data)
        {
            return new MealQueryModel
            {
                Id = data.Id
            };
        }

        protected override MealQueryModel ConvertToQuery(int id)
        {
            return new MealQueryModel { Id = id };
        }

        protected override MealQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new MealQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Meal, MealQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateMealQueryRepositoryInstance(factory);
        }

        protected override int GetId(Meal result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                context.Restaurant.Add(new RestaurantEntity()
                {
                    Id = 1,
                    Name = "Test restaurant"
                });

                var foods = Enumerable.Range(1, count).Select(x => new FoodEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    Description = $"Description {x}",
                    Price = x,
                    Type = FoodType.MAIN_COURSE,
                    RestaurantId = 1
                });

                context.Food.AddRange(foods);

                var meals = Enumerable.Range(1, count).Select(x => new MealEntity
                {
                    Id = x,
                    Price = x,
                    FoodEntityMealEntities = new List<FoodEntityMealEntities>
                    {
                        new FoodEntityMealEntities { FoodEntityId = x }
                    }
                });

                context.Meal.AddRange(meals);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public List<Food> Foods { get; set; } = new List<Food>();

            public decimal Price { get; set; }
        }
    }
}
