using System;
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
    public sealed class CustomerCommandRepositoryTest : CommandRepositoryTests<CustomerCommandRepositoryTest.Data, long, CustomerInsertModel, CustomerUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
             Enumerable.Range(1, int.MaxValue).Select(content => new Data
             {
                 Id = content,
                 Name = $"Name {content}",
                 AppUserId = $"AppUserId {1003 + content}",
                 Balance = 3.3m * content,
                 DefaultLocationId = content,
                 Role = content
             });

        protected override IDatabaseCommandRepository<long, CustomerInsertModel, CustomerUpdateModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateOnlyCustomerCommandRepositoryInstanceNoData(factory);
        }

        protected override long GetId(Either<Error, long> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var locations = Enumerable.Range(1, count + 6)
                   .Select(x => new LocationEntity()
                   {
                       Id = x,
                       Name = $"Name {x}",
                   });
                context.Location.AddRange(locations);

                var random = new Random();

                var customers = Enumerable.Range(1, count)
                   .Select(x => new CustomerEntity()
                   {
                       Id = x,
                       Balance = x,
                       GoogleUserId = (1000 + x).ToString(),
                       DefaultLocationId = x,
                       Name = $"Name {x}",
                       Role = random.Next(0, 1)
                   });
                context.Customer.AddRange(customers);
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
                DefaultLocationId = data.DefaultLocationId,
                Role = data.Role
            };
        }

        protected override CustomerUpdateModel ConvertToUpdate(Data data)
        {
            return new CustomerUpdateModel
            {
                Name = data.Name,
                DefaultLocationId = data.DefaultLocationId,
                Balance = data.Balance,
                GoogleUserId = data.AppUserId,
                Role = data.Role
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

        protected override long GetUnExistingId()
        {
            return 99999;
        }

        public sealed class Data
        {
            public long? Id { get; set; }

            public string Name { get; set; }

            public decimal Balance { get; set; }

            public long DefaultLocationId { get; set; }

            public string AppUserId { get; set; }

            public int Role { get; set; }
        }
    }
}
