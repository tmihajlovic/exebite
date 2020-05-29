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
    public sealed class CustomerQueryRepositoryTest : QueryRepositoryTests<CustomerQueryRepositoryTest.Data, Customer, CustomerQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content
            });

        protected override CustomerQueryModel ConvertEmptyToQuery()
        {
            return new CustomerQueryModel();
        }

        protected override CustomerQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override CustomerQueryModel ConvertToQuery(Data data)
        {
            return new CustomerQueryModel { Id = data.Id };
        }

        protected override CustomerQueryModel ConvertToQuery(long id)
        {
            return new CustomerQueryModel { Id = id };
        }

        protected override CustomerQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new CustomerQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Customer, CustomerQueryModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateOnlyCustomerQueryRepositoryInstanceNoData(factory);
        }

        protected override long GetId(Customer result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var random = new Random();

                var customers = Enumerable.Range(1, count)
                   .Select(x => new CustomerEntity()
                   {
                       Id = x,
                       Balance = x,
                       GoogleUserId = (1000 + x).ToString(),
                       DefaultLocationId = x,
                       DefaultLocation = new LocationEntity { Id = x, Address = $"Address {x}", Name = $"Name {x}" },
                       Name = $"Name {x}",
                       Role = random.Next()
                   });
                context.Customer.AddRange(customers);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public long? Id { get; set; }
        }
    }
}
