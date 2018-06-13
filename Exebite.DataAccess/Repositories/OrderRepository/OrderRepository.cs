using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class OrderRepository : DatabaseRepository<Order, OrderEntity, OrderQueryModel>, IOrderRepository
    {

        public OrderRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {

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
                    var order = _mapper.Map<Order>(orderEntity);
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

                return _mapper.Map<Order>(order);
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
                    var order = _mapper.Map<Order>(orderEntity);
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
                var orderEntity = new OrderEntity()
                {
                    CustomerId = entity.CustomerId,
                    Date = entity.Date,
                    MealId = entity.MealId,
                    Note = entity.Note,
                    Price = entity.Price,

                };

                var resultEntity = context.Orders.Add(orderEntity);
                context.SaveChanges();

                return _mapper.Map<Order>((OrderEntity)resultEntity.Entity);
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
                var orderEntity = _mapper.Map<OrderEntity>(entity);
                context.Update(orderEntity.Meal);
                var oldEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                context.Entry(oldEntity).CurrentValues.SetValues(orderEntity);
                context.SaveChanges();

                var resultEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                return _mapper.Map<Order>(resultEntity);
            }
        }

        public override IList<Order> Query(OrderQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Orders.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Order>>(results);
            }
        }
    }
}
