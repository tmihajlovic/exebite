using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class OrderRepository : DatabaseRepository<Order, OrderEntity, OrderQueryModel>, IOrderRepository
    {
        public OrderRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<OrderRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public IEnumerable<Order> GetOrdersForCustomer(int customerId)
        {
            using (var context = _factory.Create())
            {
                var orderList = context.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Select(x => _mapper.Map<Order>(x))
                    .ToList();
                return orderList;
            }
        }

        public Order GetOrderForCustomer(int orderId, int customerId)
        {
            using (var context = _factory.Create())
            {
                {
                    var order = context.Orders.FirstOrDefault(o => o.CustomerId == customerId && o.Id == orderId);
                    if (order == null)
                    {
                        return null;
                    }
                    else
                    {
                        return _mapper.Map<Order>(order);
                    }
                }
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

                return _mapper.Map<Order>(resultEntity.Entity);
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
                var orderEntity = new OrderEntity()
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Date = entity.Date,
                    MealId = entity.MealId,
                    Note = entity.Note,
                    Price = entity.Price
                };

                var res = context.Update(orderEntity);

                context.SaveChanges();

                return _mapper.Map<Order>(res.Entity);
            }
        }

        public override IList<Order> Query(OrderQueryModel queryModel)
        {
            queryModel = queryModel ?? new OrderQueryModel();

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

        public IEnumerable<Order> GetOrdersForDate(DateTime date)
        {
            using (var context = _factory.Create())
            {
                var orderList = context.Orders
                                       .Where(o => o.Date == date)
                                       .Select(x => _mapper.Map<Order>(x))
                                       .ToList();

                return orderList;
            }
        }
    }
}
