using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class OrderRepository : DatabaseRepository<Order, OrderEntity>, IOrderRepository
    {
        private readonly IFoodOrderingContextFactory _factory;

        public OrderRepository(IFoodOrderingContextFactory factory, IExebiteMapper mapper)
            : base(factory,mapper)
        {
            _factory = factory;
        }

        public IEnumerable<Order> GetOrdersForCustomer(int customerId)
        {
            using (var context = _factory.Create())
            {
                var customer = context.Customers.Find(customerId);
                if (customer == null)
                {
                    throw new ArgumentException("Non existing customer!");
                }

                var orderEntityList = context.Orders.Where(o => o.CustomerId == customerId);

                var orderList = new List<Order>();

                foreach (var orderEntity in orderEntityList)
                {
                    var order = AutoMapperHelper.Instance.GetMappedValue<Order>(orderEntity, context);
                    orderList.Add(order);
                }

                return orderList;
            }
        }

        public Order GetOrderForCustomer(int orderId, int customerId)
        {
            using (var context = _factory.Create())
            {
                var customer = context.Customers.Find(customerId);
                if (customer == null)
                {
                    throw new ArgumentException("Non existing customer!");
                }

                var order = context.Orders.FirstOrDefault(o => o.CustomerId == customerId && o.Id == orderId);
                if (order == null)
                {
                    return null;
                }

                return AutoMapperHelper.Instance.GetMappedValue<Order>(order, context);
            }
        }

        public IEnumerable<Order> GetOrdersForDate(DateTime date)
        {
            using (var context = _factory.Create())
            {
                var orderEntityList = context.Orders.Where(o => o.Date == date);
                var orderList = new List<Order>();
                foreach (var orderEntity in orderEntityList)
                {
                    var order = AutoMapperHelper.Instance.GetMappedValue<Order>(orderEntity, context);
                    orderList.Add(order);
                }

                return orderList;
            }
        }

        public override Order Insert(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var orderEntity = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(entity, context);

                var resultEntity = context.Update(orderEntity).Entity;
                context.SaveChanges();

                return AutoMapperHelper.Instance.GetMappedValue<Order>(resultEntity, context);
            }
        }

        public override Order Update(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var orderEntity = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(entity, context);
                context.Update(orderEntity.Meal);
                var oldEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                context.Entry(oldEntity).CurrentValues.SetValues(orderEntity);
                context.SaveChanges();

                var resultEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                return AutoMapperHelper.Instance.GetMappedValue<Order>(resultEntity, context);
            }
        }
    }
}
