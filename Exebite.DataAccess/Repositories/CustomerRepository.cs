using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerRepository : DatabaseRepository<Customer, CustomerEntity>, ICustomerRepository
    {
        private readonly IFoodOrderingContextFactory _factory;

        public CustomerRepository(IFoodOrderingContextFactory factory, IExebiteMapper mapper)
            : base(factory, mapper)
        {
            _factory = factory;
        }

        public override IList<Customer> GetAll()
        {
            using (var context = _factory.Create())
            {
                var items = context.Customers
                                .ToList();

                // hack, ProjectTo not working
                return items.Select(x => _exebiteMapper.Map<Customer>(x)).ToList();
            }
        }


        public Customer GetByName(string name)
        {
            if (name == string.Empty)
            {
                throw new System.ArgumentException("Name can't be empty string");
            }

            using (var context = _factory.Create())
            {
                var customerEntity = context.Customers.FirstOrDefault(ce => ce.Name == name);
                if (customerEntity != null)
                {
                    return AutoMapperHelper.Instance.GetMappedValue<Customer>(customerEntity, context);
                }
                else
                {
                    return null;
                }
            }
        }

        public override Customer Insert(Customer customer)
        {
            if (customer == null)
            {
                throw new System.ArgumentNullException(nameof(customer));
            }

            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(customer, context);
                var resultEntity = context.Customers.Update(customerEntity).Entity;
                context.SaveChanges();
                return AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity, context);
            }
        }

        public override Customer Update(Customer entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var customerEntity = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(entity, context);
                context.Attach(customerEntity);
                context.SaveChanges();
                var resultEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                return AutoMapperHelper.Instance.GetMappedValue<Customer>(resultEntity, context);
            }
        }
    }
}
