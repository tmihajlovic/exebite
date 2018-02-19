using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.Business
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int Id);
        Customer GetCustomerByName(string name);
        List<Customer> GetAllCustomers();
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
        
    }
}
