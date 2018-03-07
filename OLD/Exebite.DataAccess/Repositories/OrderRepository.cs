using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.DataAccess.Repositories
{
    public class OrderRepository : DatabaseRepository<Order,OrderEntity>, IOrderRepository
    {
        IFoodOrderingContextFactory _factory;
        ICustomerRepository _cusomerHandler;

        public OrderRepository(IFoodOrderingContextFactory factory, ICustomerRepository customerHandler)
            :base(factory)
        {
            _cusomerHandler = customerHandler;
            _factory = factory;
        }

        public IEnumerable<Order> GetOrdersForCustomer(Customer customer)
        {
            using (var context = _factory.Create())
            {
                var orderEntityList = context.Orders.Where(o => 
                    o.Customer.Balance == customer.Balance 
                    && o.Customer.Name == customer.Name
                    && o.Customer.Location.Name == customer.Location.Name
                    && o.Customer.Location.Address == customer.Location.Address
                );

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
                var customer = context.Customers.FirstOrDefault(c => c.Name == orderEntity.Customer.Name);
                //bind customer
                if (customer != null)
                {
                    orderEntity.Customer = customer;
                }
                else
                {
                    orderEntity.Customer = new CustomerEntity
                    {
                        Name = orderEntity.Customer.Name,
                        Balance = 0
                    };
                    if(orderEntity.Customer.Name.Contains("JD"))
                    {
                        orderEntity.Customer.LocationId = 2;
                    }
                    else
                    {
                        orderEntity.Customer.LocationId = 1;
                    }
                }
                //bind Foods
                for(int i=0; i < orderEntity.Meal.Foods.Count; i++)
                {
                    string name = orderEntity.Meal.Foods[i].Name;
                    var tmpFood = context.Foods.FirstOrDefault(f => f.Name == name);
                    orderEntity.Meal.Foods[i] = tmpFood;
                }

                var resultEntity = context.Orders.Add(orderEntity);
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
                var oldOredeEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                context.Entry(oldOredeEntity).CurrentValues.SetValues(orderEntity);
                var customer = context.Customers.FirstOrDefault(c => c.Name == orderEntity.Customer.Name);

                //bind customer
                if (customer != null)
                {
                    orderEntity.Customer = customer;
                }
                else
                {
                    orderEntity.Customer = new CustomerEntity
                    {
                        Name = orderEntity.Customer.Name,
                        Balance = 0
                    };
                    if (orderEntity.Customer.Name.Contains("JD"))
                    {
                        orderEntity.Customer.LocationId = 2;
                    }
                    else
                    {
                        orderEntity.Customer.LocationId = 1;
                    }
                }
                var restName = orderEntity.Meal.Foods[0].Restaurant.Name;
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Name == restName);
                //bind Foods
                for (int i = 0; i < orderEntity.Meal.Foods.Count; i++)
                {
                    string name = orderEntity.Meal.Foods[i].Name;
                    var tmpFood = context.Foods.FirstOrDefault(f => f.Name == name);
                    if (tmpFood != null)
                    {
                        orderEntity.Meal.Foods[i] = tmpFood;
                    }
                    else
                    {
                        if (restaurant != null)
                        {
                            orderEntity.Meal.Foods[i].Restaurant = restaurant;
                        }
                    }
                }
                context.SaveChanges();

                var resultEntity = context.Orders.FirstOrDefault(o => o.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Order>(resultEntity);
                return result;
            }
        }
    }
}
