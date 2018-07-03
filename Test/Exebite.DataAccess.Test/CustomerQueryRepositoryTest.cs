using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
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

        protected override CustomerQueryModel ConvertToQuery(int id)
        {
            return new CustomerQueryModel { Id = id };
        }

        protected override CustomerQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new CustomerQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Customer, CustomerQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyCustomerQueryRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Customer result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var customers = Enumerable.Range(1, count)
                   .Select(x => new CustomerEntity()
                   {
                       Id = x,
                       Balance = x,
                       AppUserId = (1000 + x).ToString(),
                       LocationId = x,
                       Location = new LocationEntity { Id = x, Address = $"Address {x}", Name = $"Name {x}" },
                       Name = $"Name {x}",
                   });
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }
        }
    }
}
