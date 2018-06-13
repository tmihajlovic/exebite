using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerAliasRepository : DatabaseRepository<CustomerAliases, CustomerAliasesEntities, CustomerAliasQueryModel>, ICustomerAliasRepository
    {
        public CustomerAliasRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {
        }

        public override CustomerAliases Insert(CustomerAliases entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var aliasEntity = _mapper.Map<CustomerAliasesEntities>(entity);
                var resultEntity = context.CustomerAliases.Update(aliasEntity).Entity;
                context.SaveChanges();
                return _mapper.Map<CustomerAliases>(resultEntity);
            }
        }

        public override IList<CustomerAliases> Query(CustomerAliasQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.CustomerAliases.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<CustomerAliases>>(results);
            }
        }

        public override CustomerAliases Update(CustomerAliases entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var aliasEntity = new CustomerAliasesEntities
                {
                    Id = entity.Id,
                    Alias = entity.Alias,
                    CustomerId = entity.Customer.Id,
                    RestaurantId = entity.Restaurant.Id
                };

                context.Update(aliasEntity);
                context.SaveChanges();
                var resultEntity = context.CustomerAliases.FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<CustomerAliases>(resultEntity);
            }
        }
    }
}
