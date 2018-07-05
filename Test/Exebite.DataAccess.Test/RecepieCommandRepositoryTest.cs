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
    public sealed class RecepieCommandRepositoryTest : CommandRepositoryTests<RecepieCommandRepositoryTest.Data, int, RecipeInsertModel, RecipeUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          RestaurantId = content,
                          MainCourseId = content
                      });

        protected override IDatabaseCommandRepository<int, RecipeInsertModel, RecipeUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyRecipeCommandRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                //var restourant = new RestaurantEntity()
                //{
                //    Name = "Restaurant name " + 1
                //};



                var restaurants = Enumerable.Range(1, count + 6)
                                            .Select(x => new RestaurantEntity()
                                            {
                                                Name = "Restaurant name " + x
                                            });

                context.Restaurants.AddRange(restaurants);
                context.SaveChanges();

                var foods = Enumerable.Range(1, count + 6).Select(x => new FoodEntity()
                {
                    Name = "mainFood" + x,
                    Description = "Description" + x,
                    Price = x * 100,
                    RestaurantId = 1
                });
                context.Foods.AddRange(foods);
                context.SaveChanges();
            }
        }

        protected override RecipeInsertModel ConvertToInput(Data data)
        {
            return new RecipeInsertModel { RestaurantId = data.RestaurantId, MainCourseId = data.MainCourseId };
        }

        protected override RecipeUpdateModel ConvertToUpdate(Data data)
        {
            return new RecipeUpdateModel { RestaurantId = data.RestaurantId, MainCourseId = data.MainCourseId };
        }

        protected override RecipeInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RecipeUpdateModel ConvertToInvalidUpdate(Data data)
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
            public int RestaurantId { get; set; }

            public int MainCourseId { get; set; }
        }
    }
}
