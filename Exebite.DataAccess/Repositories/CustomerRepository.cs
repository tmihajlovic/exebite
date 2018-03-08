using System.Linq;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerRepository : DatabaseRepository<Customer,CustomerEntity>, ICustomerRepository
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
                customerEntity.Location = context.Locations.Find(customerEntity.LocationId);

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
                var oldCustomerEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                oldCustomerEntity.LocationId = customerEntity.Location.Id;
                context.Entry(oldCustomerEntity).CurrentValues.SetValues(customerEntity);

                // bind db values
                oldCustomerEntity.Location = context.Locations.Find(oldCustomerEntity.LocationId);
                for (int i = 0; i < customerEntity.Aliases.Count; i++)
                {
                    var restId = customerEntity.Aliases[i].Restaurant.Id;
                    customerEntity.Aliases[i].Restaurant = context.Restaurants.First(r => r.Id == restId);
                    customerEntity.Aliases[i].Customer = oldCustomerEntity;
                }

                oldCustomerEntity.Aliases.Clear();
                oldCustomerEntity.Aliases.AddRange(customerEntity.Aliases);

                context.SaveChanges();
                var resultEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity);
                return result;
            }
        }
    }
}
