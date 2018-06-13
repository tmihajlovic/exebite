using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerRepository : DatabaseRepository<Customer, CustomerEntity, CustomerQueryModel>, ICustomerRepository
    {
        public CustomerRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {
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
                    return _mapper.Map<Customer>(customerEntity);
                }
                else
                {
                    return null;
                }
            }
        }

        public override Customer Insert(Customer entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var customerEntity = _mapper.Map<CustomerEntity>(entity);
                var resultEntity = context.Customers.Update(customerEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<Customer>(resultEntity);
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
                var customerEntity = _mapper.Map<CustomerEntity>(entity);
                context.Attach(customerEntity);
                context.SaveChanges();
                var resultEntity = context.Customers.FirstOrDefault(c => c.Id == entity.Id);
                return _mapper.Map<Customer>(resultEntity);
            }
        }

        public override IList<Customer> Query(CustomerQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Customers.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Customer>>(results);
            }
        }
    }
}
