using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class OrderService : IOrderService
    {
        IOrderHandler _orderHandler;
        ICustomerHandler _customerHandler;
        IRestaurantHandler _restaurantHandler;

        public OrderService(IOrderHandler orderHandler, ICustomerHandler customerHandler, IRestaurantHandler restaurantHandler)
        {
            _orderHandler = orderHandler;
            _customerHandler = customerHandler;
            _restaurantHandler = restaurantHandler;
        }

        public List<Order> GetAllOrders()
        {
             return _orderHandler.Get().ToList();
        }

        public List<Order> GetAllOrdersForCustomer(int customerId)
        {
            var customer = _customerHandler.GetByID(customerId);
            if (customer == null)
            {
                return null;
            }
            return _orderHandler.GetOrdersForCustomer(customer).ToList();

        }

        public List<Order> GetAllOrdersForRestoraunt(int restorauntId)
        {
            var allOrders = this.GetAllOrders();
            return allOrders.Where(o => o.Meal.Foods[0].Restaurant.Id == restorauntId).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderHandler.GetByID(orderId);
        }

        public List<Order> GettOrdersForDate(DateTime date)
        {
            return _orderHandler.GetOrdersForDate(date).ToList();
        }

        public void PlaceOreder(Order order)
        {
            _orderHandler.Insert(order);
            
        }

        public void EditOrder(Order order)
        {
            _orderHandler.Update(order);
        }

        public void CancelOrder(int orderId)
        {
            _orderHandler.Delete(orderId);
        }
    }
}
