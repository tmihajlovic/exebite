using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderHandler;

        public OrderService(IOrderRepository orderHandler)
        {
            _orderHandler = orderHandler;
        }

        public List<Order> GetAllOrders()
        {
             return _orderHandler.GetAll().ToList();
        }

        public List<Order> GetAllOrdersForCustomer(int customerId)
        {
            return _orderHandler.GetOrdersForCustomer(customerId).ToList();
        }

        public List<Order> GetAllOrdersForRestoraunt(int restorauntId)
        {
            var allOrders = this.GetAllOrders();
            return allOrders.Where(o => o.Meal.Foods.First().Restaurant.Id == restorauntId).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderHandler.GetByID(orderId);
        }

        public List<Order> GetOrdersForDate(DateTime date)
        {
            if (date > DateTime.Today)
            {
                throw new ArgumentException("Date can't be in furure");
            }

            return _orderHandler.GetOrdersForDate(date).ToList();
        }

        public Order PlaceOreder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return _orderHandler.Insert(order);
        }

        public Order EditOrder(Order order)
        {
            return _orderHandler.Update(order);
        }

        public void CancelOrder(int orderId)
        {
            var order = _orderHandler.GetByID(orderId);
            if (order != null)
            {
                _orderHandler.Delete(orderId);
            }
        }
    }
}
