using System.Collections.Generic;
using System.Linq;
using Exebite.Common;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.BaseTests;
using Exebite.DataAccess.Test.Mocks;
using Exebite.DomainModel;
using static Exebite.DataAccess.Test.RepositoryTestHelpers;

namespace Exebite.DataAccess.Test
{
    public sealed class OrderQueryRepositoryTest : QueryRepositoryTests<OrderQueryRepositoryTest.Data, Order, OrderQueryModel>
    {
        private readonly IGetDateTime _dateTime;

        public OrderQueryRepositoryTest()
        {
            _dateTime = new GetDateTimeStub();
        }

        protected override IEnumerable<Data> SampleData =>
            Enumerable.Range(1, int.MaxValue).Select(content => new Data
            {
                Id = content
            });

        protected override OrderQueryModel ConvertEmptyToQuery()
        {
            return new OrderQueryModel();
        }

        protected override OrderQueryModel ConvertNullToQuery()
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        protected override OrderQueryModel ConvertToQuery(Data data)
        {
            return new OrderQueryModel
            {
                Id = data.Id
            };
        }

        protected override OrderQueryModel ConvertToQuery(int id)
        {
            return new OrderQueryModel { Id = id };
        }

        protected override OrderQueryModel ConvertWithPageAndSize(int page, int size)
        {
            return new OrderQueryModel(page, size);
        }

        protected override IDatabaseQueryRepository<Order, OrderQueryModel> CreateSut(IFoodOrderingContextFactory factory)
        {
            return CreateOrderQueryRepositoryInstance(factory);
        }

        protected override int GetId(Order result)
        {
            return result.Id;
        }

        protected override void InitializeStorage(IFoodOrderingContextFactory factory, int count)
        {
            using (var context = factory.Create())
            {
                var location = new LocationEntity
                {
                    Id = 1,
                    Name = "location name ",
                    Address = "Address"
                };
                context.Location.Add(location);

                var roles = Enumerable.Range(1, count + 6).Select(x => new RoleEntity
                {
                    Id = x,
                    Name = $"role name {x}"
                });
                context.Role.AddRange(roles);

                var customers = Enumerable.Range(1, count).Select(x => new CustomerEntity
                {
                    Id = x,
                    Name = "Customer name ",
                    GoogleId = "GoogleUserId",
                    Balance = 99.99m,
                    LocationId = 1,
                    RoleId = x
                });
                context.Customer.AddRange(customers);

                var meals = Enumerable.Range(1, count).Select(x => new MealEntity
                {
                    Id = x,
                    Price = 3.2m * x
                });
                context.Meal.AddRange(meals);

                var orders = Enumerable.Range(1, count).Select(x => new OrderEntity
                {
                    Id = x,
                    CustomerId = x,
                    Date = _dateTime.Now().AddHours(x),
                    MealId = x,
                    Note = "note ",
                    Price = 10.5m * x
                });
                context.Order.AddRange(orders);

                context.SaveChanges();
            }
        }

        public sealed class Data
        {
            public int? Id { get; set; }
        }
    }
}
