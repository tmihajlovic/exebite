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
    public sealed class RecipeQueryRepositoryTest : QueryRepositoryTests<RecipeQueryRepositoryTest.Data, Recipe, RecipeQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                Name = $"Name {content}"
            });

        protected override RecipeQueryModel ConvertEmptyToQuery()
        {
            return new RecipeQueryModel();
        }

        protected override RecipeQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override RecipeQueryModel ConvertToQuery(Data data)
        {
            return new RecipeQueryModel
            {
                Id = data.Id,
            };
        }

        protected override RecipeQueryModel ConvertToQuery(int id)
        {
            return new RecipeQueryModel { Id = id };
        }

        protected override RecipeQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new RecipeQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Recipe, RecipeQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyRecipeQueryRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Recipe result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var recipes = Enumerable.Range(1, count).Select(x => new RecipeEntity
                {
                    Id = x,
                    RestaurantId = x,
                    MainCourseId = x
                });
                context.Recipes.AddRange(recipes);

                var dailyMenus = Enumerable.Range(1, count).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, count).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = "Test restaurant " + x
                });
                context.Restaurants.AddRange(restaurant);

                var food = Enumerable.Range(1, count).Select(x => new FoodEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}",
                    RestaurantId = x
                });
                context.Foods.AddRange(food);
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
