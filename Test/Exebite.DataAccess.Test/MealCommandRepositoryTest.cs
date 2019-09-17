using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Exebite.DomainModel;
using Optional.Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class MealCommandRepositoryTest : CommandRepositoryTests<MealCommandRepositoryTest.Data, int, MealInsertModel, MealUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          Id = content,
                          Price = 2.3m * content,
                          Foods = new List<int> { content }
                      });

        protected override IDatabaseCommandRepository<int, MealInsertModel, MealUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateMealCommandRepositoryInstance(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
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

                var foods = Enumerable.Range(1, count + 6).Select(x => new FoodEntity()
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
                    FoodEntityMealEntities = new List<FoodEntityMealEntity>
                    {
                        new FoodEntityMealEntity { FoodEntityId = x }
                    }
                });

                context.Meal.AddRange(meals);
                context.SaveChanges();
            }
        }

        protected override MealInsertModel ConvertToInput(Data data)
        {
            return new MealInsertModel
            {
                Price = data.Price,
                Foods = data.Foods
            };
        }

        protected override MealUpdateModel ConvertToUpdate(Data data)
        {
            return new MealUpdateModel
            {
                Price = data.Price,
                Foods = data.Foods
            };
        }

        protected override MealInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override MealUpdateModel ConvertToInvalidUpdate(Data data)
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
            public int? Id { get; set; }

            public List<int> Foods { get; set; } = new List<int>();

            public decimal Price { get; set; }
        }
    }
}
