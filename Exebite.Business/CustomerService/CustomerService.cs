using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class CustomerService : ICustomerService
    {

        ICustomerRepository _customerHandler;

        public CustomerService(ICustomerRepository customerHandler)
        {
            _customerHandler = customerHandler;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerHandler.GetAll().ToList();
        }

        public Customer GetCustomerById(int Id)
        {
            return _customerHandler.GetByID(Id);
        }

        public Customer GetCustomerByName(string name)
        {
            return _customerHandler.GetByName(name);
        }

        public void CreateCustomer(Customer customer)
        {
            _customerHandler.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerHandler.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerHandler.Delete(customerId);
        }
    }
}
