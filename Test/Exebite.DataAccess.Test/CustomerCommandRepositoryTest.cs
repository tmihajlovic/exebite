using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Optional.Xunit;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class CustomerCommandRepositoryTest : CommandRepositoryTests<CustomerCommandRepositoryTest.Data, int, CustomerInsertModel, CustomerUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
             Enumerable.Range(1, int.MaxValue).Select(content => new Data
             {
                 Id = content,
                 Name = $"Name {content}",
                 AppUserId = $"AppUserId {1003 + content}",
                 Balance = 3.3m * content,
                 LocationId = content,
                 RoleId = content
             });

        protected override IDatabaseCommandRepository<int, CustomerInsertModel, CustomerUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyCustomerCommandRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count + 6)
                   .Select(x => new LocationEntity()
                   {
                       Id = x,
                       Name = $"Name {x}",
                   });
                context.Locations.AddRange(locations);

                var roles = Enumerable.Range(1, count + 6)
                   .Select(x => new RoleEntity()
                   {
                       Id = x,
                       Name = $"Name {x}",
                   });
                context.Roles.AddRange(roles);

                var customers = Enumerable.Range(1, count)
                   .Select(x => new CustomerEntity()
                   {
                       Id = x,
                       Balance = x,
                       GoogleUserId = (1000 + x).ToString(),
                       LocationId = x,
                       Name = $"Name {x}",
                       RoleId = x
                   });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        protected override CustomerInsertModel ConvertToInput(Data data)
        {
            return new CustomerInsertModel
            {
                Name = data.Name,
                GoogleUserId = data.AppUserId,
                Balance = data.Balance,
                LocationId = data.LocationId,
                RoleId = data.RoleId
            };
        }

        protected override CustomerUpdateModel ConvertToUpdate(Data data)
        {
            return new CustomerUpdateModel
            {
                Name = data.Name,
                LocationId = data.LocationId,
                Balance = data.Balance,
                GoogleUserId = data.AppUserId,
                RoleId = data.RoleId
            };
        }

        protected override CustomerInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override CustomerUpdateModel ConvertToInvalidUpdate(Data data)
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

            public string Name { get; set; }

            public decimal Balance { get; set; }

            public int LocationId { get; set; }

            public string AppUserId { get; set; }

            public int RoleId { get; set; }
        }
    }
}
