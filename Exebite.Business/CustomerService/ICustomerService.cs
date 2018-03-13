using System;
using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get <see cref="Customer"/> by Id
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <returns>Customer with given Id</returns>
        Customer GetCustomerById(int id);

        /// <summary>
        /// Get <see cref="Customer"/> with given user idenity Id
        /// </summary>
        /// <param name="id">Identity Id of User</param>
        /// <returns>Customer with linked user account</returns>
        Customer GetCustomerByIdentityId(string id);

        /// <summary>
        /// Gets <see cref="Customer"/> by name
        /// </summary>
        /// <param name="name">Name of customer</param>
        /// <returns>Customer with given name</returns>
        Customer GetCustomerByName(string name);

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>List of all customers</returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// Create new <see cref="Customer"/>
        /// </summary>
        /// <param name="customer">Customer to be created</param>
        /// <returns>New Customer from database</returns>
        Customer CreateCustomer(Customer customer);

        /// <summary>
        /// Update customer data
        /// </summary>
        /// <param name="customer">Customer with new data</param>
        /// <returns>Updated customer from database</returns>
        Customer UpdateCustomer(Customer customer);

        /// <summary>
        /// Delete customer from database
        /// </summary>
        /// <param name="customerId">Id of customer</param>
        void DeleteCustomer(int customerId);
    }
}
