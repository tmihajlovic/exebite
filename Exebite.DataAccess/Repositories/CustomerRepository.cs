using System.Linq;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerRepository : DatabaseRepository<Customer, CustomerEntity>, ICustomerRepository
    {
        private IFoodOrderingContextFactory _factory;

        public CustomerRepository(IFoodOrderingContextFactory factory)
            : base(factory)
        {
            this._factory = factory;
        }

        public Customer GetByName(string name)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = context.Customers.FirstOrDefault(ce => ce.Name == name);
                if (customerEntity != null)
                {
                    var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(customerEntity);
                    return customer;
                }
                else
                {
                    return null;
                }
            }
        }

        public override Customer Insert(Customer customer)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(customer);
                var resultEntity = context.Customers.Update(customerEntity).Entity;
                context.SaveChanges();
                var result = AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity);
                return result;
            }
        }

        public override Customer Update(Customer entity)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(entity);
                context.Attach(customerEntity);
                context.SaveChanges();
                var resultEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity);
                return result;
            }
        }
    }
}
