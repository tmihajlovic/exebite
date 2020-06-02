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
    public sealed class MealCommandRepositoryTest : CommandRepositoryTests<MealCommandRepositoryTest.Data, long, MealInsertModel, MealUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          Id = content,
                          Price = 2.3m * content,
                          Name = $"Name {content}",
                          Description = $"Description {content}",
                          Type = (int)MealType.MAIN_COURSE,
                          RestaurantId = 1,
                          IsActive = true,
                          Note = $"Note {content}"
                      });

        protected override IDatabaseCommandRepository<long, MealInsertModel, MealUpdateModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateMealCommandRepositoryInstance(factory);
        }

        protected override long GetId(Either<Error, long> newObj)
        {
            return newObj.RightContent();
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

        protected override MealInsertModel ConvertToInput(Data data)
        {
            return new MealInsertModel
            {
                Name = data.Name,
                Description = data.Description,
                Type = (MealType)data.Type,
                RestaurantId = data.RestaurantId,
                IsActive = data.IsActive,
                Note = data.Note,
                Price = data.Price
            };
        }

        protected override MealUpdateModel ConvertToUpdate(Data data)
        {
            return new MealUpdateModel
            {
                Name = data.Name,
                Description = data.Description,
                Type = (MealType)data.Type,
                RestaurantId = data.RestaurantId,
                IsActive = data.IsActive,
                Note = data.Note,
                Price = data.Price
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

        protected override long GetUnExistingId()
        {
            return 99999;
        }

        public sealed class Data
        {
            public long? Id { get; set; }

            public string Name { get; set; }

            public int Type { get; set; }

            public decimal Price { get; set; }

            public string Description { get; set; }

            public string Note { get; set; }

            public bool IsActive { get; set; }

            public long RestaurantId { get; set; }
        }
    }
}
