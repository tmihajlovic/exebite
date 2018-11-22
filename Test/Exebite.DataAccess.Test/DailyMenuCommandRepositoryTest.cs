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
    public sealed class DailyMenuCommandRepositoryTest : CommandRepositoryTests<DailyMenuCommandRepositoryTest.Data, int, DailyMenuInsertModel, DailyMenuUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                 Enumerable.Range(1, int.MaxValue).Select(content => new Data
                 {
                     Id = content,
                     RestaurantId = content,
                     Foods = new List<Food> { new Food { Id = content } }
                 });

        protected override IDatabaseCommandRepository<int, DailyMenuInsertModel, DailyMenuUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateDailyMenuCommandRepositoryInstance(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var dailyMenus = Enumerable.Range(1, count).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenu.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, count + 6).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurant.AddRange(restaurant);

                var food = Enumerable.Range(1, count + 6).Select(x => new FoodEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}",
                    RestaurantId = x
                });
                context.Food.AddRange(food);
                context.SaveChanges();
            }
        }

        protected override DailyMenuInsertModel ConvertToInput(Data data)
        {
            return new DailyMenuInsertModel
            {
                RestaurantId = data.RestaurantId,
                Foods = data.Foods
            };
        }

        protected override DailyMenuUpdateModel ConvertToUpdate(Data data)
        {
            return new DailyMenuUpdateModel
            {
                RestaurantId = data.RestaurantId,
                Foods = data.Foods
            };
        }

        protected override DailyMenuInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override DailyMenuUpdateModel ConvertToInvalidUpdate(Data data)
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

            public int RestaurantId { get; set; }

            public List<Food> Foods { get; set; }
        }
    }
}
