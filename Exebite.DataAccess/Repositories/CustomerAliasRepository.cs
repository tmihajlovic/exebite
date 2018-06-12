using System;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerAliasRepository : DatabaseRepository<CustomerAliases, CustomerAliasesEntities>, ICustomerAliasRepository
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

        public override CustomerAliases Update(CustomerAliases entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var aliasEntity = _mapper.Map<CustomerAliasesEntities>(entity);
                context.Update(aliasEntity);
                context.SaveChanges();
                var resultEntity = context.CustomerAliases.FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<CustomerAliases>(resultEntity);
            }
        }
    }
}
