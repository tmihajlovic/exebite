using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.Model;

namespace Exebite.Business
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomerByIdentityId(string id)
        {
            if (id == string.Empty)
            {
                throw new System.ArgumentException("Id cant be empty string");
            }

            var users = _customerRepository.Get(0, int.MaxValue);
            return users.FirstOrDefault(c => c.AppUserId == id);
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.Get(0, int.MaxValue).ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetByID(id);
        }

        public Customer GetCustomerByName(string name)
        {
            if (name == string.Empty)
            {
                throw new System.ArgumentException("Name cant be empty string");
            }

            return _customerRepository.GetByName(name);
        }

        public Customer CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new System.ArgumentNullException(nameof(customer));
            }

            return _customerRepository.Insert(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            var exist = _customerRepository.GetByID(customerId);
            if (exist != null)
            {
                _customerRepository.Delete(customerId);
            }
        }
    }
}
