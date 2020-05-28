using System;
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
    public sealed class DailyMenuQueryRepositoryTest : QueryRepositoryTests<DailyMenuQueryRepositoryTest.Data, DailyMenu, DailyMenuQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content
            });

        protected override DailyMenuQueryModel ConvertEmptyToQuery()
        {
            return new DailyMenuQueryModel();
        }

        protected override DailyMenuQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override DailyMenuQueryModel ConvertToQuery(Data data)
        {
            return new DailyMenuQueryModel
            {
                Id = data.Id
            };
        }

        protected override DailyMenuQueryModel ConvertToQuery(int id)
        {
            return new DailyMenuQueryModel { Id = id };
        }

        protected override DailyMenuQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new DailyMenuQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<DailyMenu, DailyMenuQueryModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateDailyMenuQueryRepositoryInstance(factory);
        }

        protected override int GetId(DailyMenu result)
        {
            return result.Id;
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

                var restaurant = Enumerable.Range(1, count).Select(x => new RestaurantEntity
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
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }
        }
    }
}
