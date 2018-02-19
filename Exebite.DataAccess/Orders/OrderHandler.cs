using Exebite.DataAccess.Context;
using Exebite.DataAccess.Customers;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Exebite.DataAccess.Orders
{
    public class OrderHandler : IOrderHandler
    {
        IFoodOrderingContextFactory _factory;
        ICustomerHandler _cusomerHandler;
        public OrderHandler(IFoodOrderingContextFactory factory, ICustomerHandler customerHandler)
        {
            _cusomerHandler = customerHandler;
            _factory = factory;
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var order = context.Orders.Find(Id);
                context.Orders.Remove(order);
                context.SaveChanges();
            }
        }

        public IEnumerable<Order> Get()
        {
            using (var context = _factory.Create())
            {
                var orderEntities = new List<Order>();

                foreach (var order in context.Orders)
                {
                    var orderModel = AutoMapperHelper.Instance.GetMappedValue<Order>(order);
                    orderEntities.Add(orderModel);
                }

                return orderEntities;
            }
        }

        public Order GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var orderEntity = context.Orders.Find(Id);
                var order = AutoMapperHelper.Instance.GetMappedValue<Order>(orderEntity);
                return order;
            }
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

        public void Insert(Order entity)
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
                var restName = orderEntity.Meal.Foods[0].Restaurant.Name;
                var restaurant = context.Restaurants.FirstOrDefault(r => r.Name == restName);
                //bind Foods
                for(int i=0; i < orderEntity.Meal.Foods.Count; i++)
                {
                    string name = orderEntity.Meal.Foods[i].Name;
                    var tmpFood = context.Foods.FirstOrDefault(f => f.Name == name);
                    if (tmpFood != null)
                    {
                        orderEntity.Meal.Foods[i] = tmpFood;
                    }
                    else
                    {
                        if(restaurant != null)
                        {
                            orderEntity.Meal.Foods[i].Restaurant = restaurant;
                        }
                    }
                }

                context.Orders.Add(orderEntity);
                context.SaveChanges();
            }
        }

        public void Update(Order entity)
        {
            using (var context = _factory.Create())
            {
                var orderEntity = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(entity);
                context.Entry(orderEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
