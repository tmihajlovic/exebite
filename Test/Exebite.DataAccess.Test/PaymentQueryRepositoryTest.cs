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
    public sealed class PaymentQueryRepositoryTest : QueryRepositoryTests<PaymentQueryRepositoryTest.Data, Payment, PaymentQueryModel>
    {
        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content,
                CustomerId = content
            });

        protected override PaymentQueryModel ConvertEmptyToQuery()
        {
            return new PaymentQueryModel();
        }

        protected override PaymentQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override PaymentQueryModel ConvertToQuery(Data data)
        {
            return new PaymentQueryModel
            {
                Id = data.Id
            };
        }

        protected override PaymentQueryModel ConvertToQuery(long id)
        {
            return new PaymentQueryModel { Id = id };
        }

        protected override PaymentQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new PaymentQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Payment, PaymentQueryModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateOnlyPaymentQueryRepositoryInstanceNoData(factory);
        }

        protected override long GetId(Payment result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IMealOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                context.Location.Add(new LocationEntity()
                {
                    Id = 1,
                    Name = "Test location"
                });

                var customers = Enumerable.Range(1, count + 6).Select(x => new CustomerEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    DefaultLocationId = 1,
                    Role = 1
                });

                context.Customer.AddRange(customers);

                var locations = Enumerable.Range(1, count).Select(x => new PaymentEntity()
                {
                    Id = x,
                    Amount = x,
                    CustomerId = x
                });

                context.Payment.AddRange(locations);
                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }

            public int CustomerId { get; set; }
        }
    }
}
