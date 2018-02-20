using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exebite.DataAccess;
using Exebite.DataAccess.Handlers;
using Exebite.Model;

namespace Exebite.Business
{
    public class CustomerService : ICustomerService
    {

        ICustomerHandler _customerHandler;

        public CustomerService(ICustomerHandler customerHandler)
        {
            _customerHandler = customerHandler;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerHandler.Get().ToList();
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
