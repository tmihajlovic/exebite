using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerHandler;

        public CustomerService(ICustomerRepository customerHandler)
        {
            _customerHandler = customerHandler;
        }

        public Customer GetCustomerByIdentityId(string id)
        {
            var users = _customerHandler.GetAll();
            return users.FirstOrDefault(c => c.AppUserId == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerHandler.GetAll().ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerHandler.GetByID(id);
        }

        public Customer GetCustomerByName(string name)
        {
            return _customerHandler.GetByName(name);
        }

        public Customer CreateCustomer(Customer customer)
        {
           return _customerHandler.Insert(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
           return _customerHandler.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerHandler.Delete(customerId);
        }
    }
}
