using Exebite.Model;
using System;
using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IOrderRepository : IDatabaseRepository<Order>
    {
        // Add functions specific for IOrderHandler
        IEnumerable<Order> GetOrdersForDate(DateTime date);

        IEnumerable<Order> GetOrdersForCustomer(Customer customer);
    }
}
