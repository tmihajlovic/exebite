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
    public sealed class CustomerAliasCommandRepositoryTest : CommandRepositoryTests<CustomerAliasCommandRepositoryTest.Data, int, CustomerAliasInsertModel, CustomerAliasUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
              Enumerable.Range(1, int.MaxValue).Select(content => new Data
              {
                  Id = content,
                  Alias = $"Alias {content}",
                  CustomerId = content,
                  RestaurantId = content
              });

        protected override CustomerAliasInsertModel ConvertToInput(Data data)
        {
            return new CustomerAliasInsertModel
            {
                Alias = data.Alias,
                CustomerId = data.CustomerId,
                RestaurantId = data.RestaurantId
            };
        }

        protected override CustomerAliasInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override CustomerAliasUpdateModel ConvertToInvalidUpdate(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override CustomerAliasUpdateModel ConvertToUpdate(Data data)
        {
            return new CustomerAliasUpdateModel
            {
                Alias = data.Alias,
                CustomerId = data.CustomerId,
                RestaurantId = data.RestaurantId
            };
        }

        protected override IDatabaseCommandRepository<int, CustomerAliasInsertModel, CustomerAliasUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateCustomerAliasCommandRepositoryInstance(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override int GetUnExistingId()
        {
            return 99999;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count + 6).Select(x => new LocationEntity()
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

                var restaurant = Enumerable.Range(1, count + 6).Select(x => new RestaurantEntity
                {
                    Id = x,
                    Name = $"Name {x}"
                });
                context.Restaurants.AddRange(restaurant);

                var customers = Enumerable.Range(1, count + 6).Select(x => new CustomerEntity
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
