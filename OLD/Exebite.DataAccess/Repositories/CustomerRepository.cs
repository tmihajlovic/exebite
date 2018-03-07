using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerRepository : DatabaseRepository<Customer,CustomerEntity>, ICustomerRepository
    {
        IFoodOrderingContextFactory _factory;
        
        public CustomerRepository(IFoodOrderingContextFactory factory)
            :base(factory)
        {
            this._factory = factory;
        }
        

        public Customer GetByName(string name)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = context.Customers.Where(ce => ce.Name == name).FirstOrDefault();
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(customerEntity);
                return customer;
            }
        }

        public override Customer Insert(Customer entity)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(entity);
                //TPMCODE 
                if(customerEntity.Location == null)
                {
                    customerEntity.Location = context.Locations.FirstOrDefault(l => l.Id == 1);
                }
                var location = context.Locations.FirstOrDefault(l => l.Name == customerEntity.Location.Name);
                if (location != null)
                {
                    customerEntity.Location = location;
                }

                var resultEntity = context.Customers.Add(customerEntity);
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

                oldCustomerEntity.Aliases.Clear();
                oldCustomerEntity.Aliases.AddRange(customerEntity.Aliases);
                //bind
                for(int i =0; i < oldCustomerEntity.Aliases.Count; i++)
                {
                    var restId =  oldCustomerEntity.Aliases[i].Restaurant.Id;
                    oldCustomerEntity.Aliases[i].Restaurant = context.Restaurants.First(r => r.Id ==restId) ;
                    oldCustomerEntity.Aliases[i].Customer = oldCustomerEntity;
                }

                context.SaveChanges();
                var resultEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                var result = AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity);
                return result;
            }
        }
    }
}
