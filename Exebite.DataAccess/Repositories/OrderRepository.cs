using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class OrderRepository : DatabaseRepository<Order, OrderEntity>, IOrderRepository
    {
        private IFoodOrderingContextFactory _factory;
        private ICustomerRepository _cusomerHandler;

        public OrderRepository(IFoodOrderingContextFactory factory, ICustomerRepository customerHandler)
            : base(factory)
        {
            _cusomerHandler = customerHandler;
            _factory = factory;
            var mapper = AutoMapperHelper.Instance;
        }

        public IEnumerable<Order> GetOrdersForCustomer(Customer customer)
        {
            using (var context = _factory.Create())
            {
                var orderEntityList = context.Orders.Where(o =>
                    o.Customer.Balance == customer.Balance
                    && o.Customer.Name == customer.Name
                    && o.Customer.Location.Name == customer.Location.Name
                    && o.Customer.Location.Address == customer.Location.Address);

                var orderList = new List<Order>();

                foreach (var orderEntity in orderEntityList)
                {
                    var order = AutoMapperHelper.Instance.GetMappedValue<Order>(orderEntity);
                    orderList.Add(order);
                }

                return orderList;
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
                    var order = AutoMapperHelper.Instance.GetMappedValue<Order>(orderEntity);
                    orderList.Add(order);
                }

                return orderList;
            }
        }

        public override Order Insert(Order entity)
        {
            using (var context = _factory.Create())
            {
                var orderEntity = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(entity);

                var resultEntity = context.Update(orderEntity);
                context.SaveChanges();

                var result = AutoMapperHelper.Instance.GetMappedValue<Order>(resultEntity);
                return result;
            }
        }

        public override Order Update(Order entity)
        {
            using (var context = _factory.Create())
            {
                var orderEntity = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(entity);
                context.Attach(orderEntity);
                context.SaveChanges();

                var resultEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Order>(resultEntity);
                return result;
            }
        }
    }
}
