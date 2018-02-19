using Exebite.Model;
using System;
using System.Collections.Generic;
namespace Exebite.DataAccess.Orders
{
    public interface IOrderHandler : IDatabaseHandler<Order>
    {
        // Add functions specific for IOrderHandler
        IEnumerable<Order> GetOrdersForDate(DateTime date);
        IEnumerable<Order> GetOrdersForCustomer(Customer customer);
    }
}
