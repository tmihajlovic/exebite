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
                Id = content,
                IsActive = true,
                Name = $"Name {content}",
                Price = content,
                RestaurantId = 1,
                Type = (int)MealType.MAIN_COURSE
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
                Id = data.Id,
                IsActive = data.IsActive,
                Name = data.Name,
                Price = data.Price,
                RestaurantId = data.RestaurantId,
                Type = data.Type
            };
        }

        protected override MealQueryModel ConvertToQuery(long id)
        {
            return new MealQueryModel { Id = id };
        }

        protected override MealQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new MealQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Meal, MealQueryModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateMealQueryRepositoryInstance(factory);
        }

        protected override long GetId(Meal result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                context.Restaurant.Add(new RestaurantEntity()
                {
                    Id = 1,
                    Name = "Test restaurant"
                });

                var meals = Enumerable.Range(1, count).Select(x => new MealEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    Description = $"Description {x}",
                    Price = x,
                    Type = (int)MealType.MAIN_COURSE,
                    RestaurantId = 1,
                    IsActive = true,
                    Note = $"Note {x}"
                });

                context.Meal.AddRange(meals);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public long? Id { get; set; }

            public long? RestaurantId { get; set; }

            public string Name { get; set; }

            public decimal? Price { get; set; }

            public bool? IsActive { get; set; }

            public int? Type { get; set; }
        }
    }
}
