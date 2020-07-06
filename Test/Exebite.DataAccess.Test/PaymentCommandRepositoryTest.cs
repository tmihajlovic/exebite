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
    public sealed class PaymentCommandRepositoryTest : CommandRepositoryTests<PaymentCommandRepositoryTest.Data, long, PaymentInsertModel, PaymentUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          Amount = content,
                          CustomerId = content
                      });

        protected override IDatabaseCommandRepository<long, PaymentInsertModel, PaymentUpdateModel> CreateSut(IMealOrderingContextFactory factory)
        {
            return CreateOnlyPaymentCommandRepositoryInstanceNoData(factory);
        }

        protected override long GetId(Either<Error, long> newObj)
        {
            return newObj.RightContent();
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

        protected override PaymentInsertModel ConvertToInput(Data data)
        {
            return new PaymentInsertModel
            {
                Amount = data.Amount,
                CustomerId = data.CustomerId
            };
        }

        protected override PaymentUpdateModel ConvertToUpdate(Data data)
        {
            return new PaymentUpdateModel
            {
                Amount = data.Amount,
                CustomerId = data.CustomerId
            };
        }

        protected override PaymentInsertModel ConvertToInvalidInput(Data data)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override PaymentUpdateModel ConvertToInvalidUpdate(Data data)
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
            public int Amount { get; set; }

            public int CustomerId { get; set; }
        }
    }
}
