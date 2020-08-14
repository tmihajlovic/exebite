using System;
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
    public sealed class DailyMenuCommandRepositoryTest : CommandRepositoryTests<DailyMenuCommandRepositoryTest.Data, long, DailyMenuInsertModel, DailyMenuUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                 Enumerable.Range(1, int.MaxValue).Select(content => new Data
                 {
                     Id = content,
                     RestaurantId = content,
                     Meals = new List<Meal> { new Meal { Id = content } }
                 });

        protected override IDatabaseCommandRepository<long, DailyMenuInsertModel, DailyMenuUpdateModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateDailyMenuCommandRepositoryInstance(factory);
        }

        protected override long GetId(Either<Error, long> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var dailyMenus = Enumerable.Range(1, count).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x,
                    Date = DateTime.UtcNow
                });
                context.DailyMenu.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, count + 6).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Email = $"Email {x}",
                    Contact = $"Contact {x}",
                    SheetId = $"SheetId {x}",
                    Description = $"Description {x}",
                    LogoUrl = $"LogoUrl {x}"
                });
                context.Restaurant.AddRange(restaurant);

                var meal = Enumerable.Range(1, count + 6).Select(x => new MealEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    Price = x,
                    Description = $"Description {x}",
                    RestaurantId = x
                });
                context.Meal.AddRange(meal);
                context.SaveChanges();
            }
        }

        protected override DailyMenuInsertModel ConvertToInput(Data data)
        {
            return new DailyMenuInsertModel
            {
                RestaurantId = data.RestaurantId,
                Meals = data.Meals
            };
        }

        protected override DailyMenuUpdateModel ConvertToUpdate(Data data)
        {
            return new DailyMenuUpdateModel
            {
                RestaurantId = data.RestaurantId,
                Meals = data.Meals
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

        protected override long GetUnExistingId()
        {
            return 99999;
        }

        public sealed class Data
        {
            public long? Id { get; set; }

            public long RestaurantId { get; set; }

            public List<Meal> Meals { get; set; }
        }
    }
}
