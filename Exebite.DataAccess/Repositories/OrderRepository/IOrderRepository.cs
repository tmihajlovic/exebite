using System;
using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public interface IOrderRepository : IDatabaseRepository<Order, OrderQueryModel>
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
        /// <param name="customerId">Id of customer</param>
        /// <returns>List of all customers orders</returns>
        IEnumerable<Order> GetOrdersForCustomer(int customerId);

        /// <summary>
        /// Get orders for given <see cref="Customer"/>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="customerId">Id of the customer</param>
        /// <returns>Order if exists for that customer, otherwise null.</returns>
        Order GetOrderForCustomer(int orderId, int customerId);
    }
}
