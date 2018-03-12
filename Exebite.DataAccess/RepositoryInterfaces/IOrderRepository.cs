using Exebite.Model;
using System;
using System.Collections.Generic;

namespace Exebite.DataAccess
{
    public interface IOrderRepository : IDatabaseRepository<Order>
    {
        // Add functions specific for IOrderHandler

        /// <summary>
        /// Get orders for given date
        /// </summary>
        /// <param name="date">Date to get orders for</param>
        /// <returns>List of orders</returns>
        IEnumerable<Order> GetOrdersForDate(DateTime date);

        /// <summary>
        /// Get orders for given <see cref="Customer"/>
        /// </summary>
        /// <param name="customer">Customer to get orders for</param>
        /// <returns>List of all customers orders</returns>
        IEnumerable<Order> GetOrdersForCustomer(Customer customer);
    }
}
