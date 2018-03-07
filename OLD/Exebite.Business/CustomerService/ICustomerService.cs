using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.Business
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int Id);
        Customer GetCustomerByIdentityId(string Id);
        Customer GetCustomerByName(string name);
        List<Customer> GetAllCustomers();
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        
    }
}
