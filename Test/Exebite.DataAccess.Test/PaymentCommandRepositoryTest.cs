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
    public sealed class PaymentCommandRepositoryTest : CommandRepositoryTests<PaymentCommandRepositoryTest.Data, int, PaymentInsertModel, PaymentUpdateModel>
    {
        protected override IEnumerable<Data> SampleData =>
                      Enumerable.Range(1, int.MaxValue).Select(content => new Data
                      {
                          Amount = content,
                          CustomerId = content
                      });

        protected override IDatabaseCommandRepository<int, PaymentInsertModel, PaymentUpdateModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOnlyPaymentCommandRepositoryInstanceNoData(factory);
        }

        protected override int GetId(Either<Error, int> newObj)
        {
            return newObj.RightContent();
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                context.Roles.Add(new RoleEntity()
                {
                    Id = 1,
                    Name = "Test Role"
                });

                context.Locations.Add(new LocationEntity()
                {
                    Id = 1,
                    Name = "Test location"
                });

                var customers = Enumerable.Range(1, count + 6).Select(x => new CustomerEntity()
                {
                    Id = x,
                    Name = $"Name {x}",
                    LocationId = 1,
                    RoleId = 1
                });

                context.Customers.AddRange(customers);

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

        protected override int GetUnExistingId()
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
