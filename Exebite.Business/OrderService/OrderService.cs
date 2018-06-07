using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll().ToList();
        }

        public List<Order> GetAllOrdersForCustomer(int customerId)
        {
            return _orderRepository.GetOrdersForCustomer(customerId).ToList();
        }

        public List<Order> GetAllOrdersForRestoraunt(int restorauntId)
        {
            var allOrders = this.GetAllOrders();
            return allOrders.Where(o => o.Meal.Foods.First().Restaurant.Id == restorauntId).ToList();
        }

        public Order GetOrderByIdForCustomer(int orderId, int customerId)
        {
            return _orderRepository.GetOrderForCustomer(orderId, customerId);
        }

        public List<Order> GetOrdersForDate(DateTime date)
        {
            if (date > DateTime.Today)
            {
                throw new ArgumentException("Date can't be in future");
            }

            return _orderRepository.GetOrdersForDate(date).ToList();
        }

        public Order CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return _orderRepository.Insert(order);
        }

        public Order UpdateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return _orderRepository.Update(order);
        }

        public void DeleteOrder(int orderId)
        {
            var order = _orderRepository.GetByID(orderId);
            if (order != null)
            {
                _orderRepository.Delete(orderId);
            }
        }
    }
}
