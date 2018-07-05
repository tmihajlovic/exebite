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
    public sealed class CustomerAliasQueryRepositoryTest : QueryRepositoryTests<CustomerAliasQueryRepositoryTest.Data, CustomerAliases, CustomerAliasQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
              Enumerable.Range(1, int.MaxValue).Select(content => new Data
              {
                  Id = content,
                  Alias = $"Alias {content}",
                  CustomerId = content,
                  RestaurantId = content
              });

        protected override CustomerAliasQueryModel ConvertEmptyToQuery()
        {
            return new CustomerAliasQueryModel();
        }

        protected override CustomerAliasQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override CustomerAliasQueryModel ConvertToQuery(Data data)
        {
            return new CustomerAliasQueryModel { Id = data.Id };
        }

        protected override CustomerAliasQueryModel ConvertToQuery(int id)
        {
            return new CustomerAliasQueryModel { Id = id };
        }

        protected override CustomerAliasQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new CustomerAliasQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<CustomerAliases, CustomerAliasQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateCustomerAliasQueryRepositoryInstance(factory);
        }

        protected override int GetId(CustomerAliases result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count).Select(x => new LocationEntity()
                {
                    Id = x,
                    Address = $"Address {x}",
                    Name = $"Name {x}"
                });

                context.Locations.AddRange(locations);
                var customerAlias = Enumerable.Range(1, count).Select(x => new CustomerAliasesEntities
                {
                    Id = x,
                    Alias = $"Alias {x}",
                    CustomerId = x,
                    RestaurantId = x
                });
                context.CustomerAliases.AddRange(customerAlias);

                var dailyMenus = Enumerable.Range(1, count).Select(x => new DailyMenuEntity
                {
                    Id = x,
                    RestaurantId = x
                });
                context.DailyMenues.AddRange(dailyMenus);

                var restaurant = Enumerable.Range(1, count).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var customers = Enumerable.Range(1, count).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = $"Name {x}",
                    LocationId = x
                });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public string Alias { get; set; }

            public int CustomerId { get; set; }

            public int RestaurantId { get; set; }
        }
    }
}
