using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Customers
{
    public class CustomerHandler : ICustomerHandler
    {
        IFoodOrderingContextFactory _factory;
        
        public CustomerHandler(IFoodOrderingContextFactory factory)
        {
            this._factory = factory;
        }


        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var customer = context.Customers.Find(Id);
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
        }

        public IEnumerable<Customer> Get()
        {
            using (var context = _factory.Create())
            {
                var customerEntities = new List<Customer>();

                foreach (var customer in context.Customers)
                {
                    var customerModel = AutoMapperHelper.Instance.GetMappedValue<Customer>(customer);
                    customerEntities.Add(customerModel);
                }

                return customerEntities;
            }
        }

        public Customer GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = context.Customers.Find(Id);
                var customer = AutoMapperHelper.Instance.GetMappedValue<Customer>(customerEntity);
                return customer;
            }
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

        public void Insert(Customer entity)
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

                context.Customers.Add(customerEntity);
                context.SaveChanges();
            }
        }
        

        public void Update(Customer entity)
        {
            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(entity);
                context.Entry(customerEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
