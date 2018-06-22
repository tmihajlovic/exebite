using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class CustomerAliasRepository : DatabaseRepository<CustomerAliases, CustomerAliasesEntities, CustomerAliasQueryModel>, ICustomerAliasRepository
    {
        public CustomerAliasRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<CustomerAliasRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public override CustomerAliases Insert(CustomerAliases entity)
        {
            _logger.LogDebug("Insert started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
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

                var createdEntity = context.CustomerAliases.Add(aliasEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Insert finished.");
                createdEntity = context.CustomerAliases.Include(a => a.Restaurant)
                                                       .Include(a => a.Customer)
                                                       .FirstOrDefault(a => a.Id == createdEntity.Id);
                return _mapper.Map<CustomerAliases>(createdEntity);
            }
        }

        public override IList<CustomerAliases> Query(CustomerAliasQueryModel queryModel)
        {
            _logger.LogDebug("Querying started.");
            if (queryModel == null)
            {
                _logger.LogError($"Argument {queryModel} is null");
                throw new ArgumentNullException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.CustomerAliases.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                _logger.LogDebug("Querying by ", queryModel.ToString());
                var results = query.ToList();
                _logger.LogDebug("Querying finished.");
                return _mapper.Map<IList<CustomerAliases>>(results);
            }
        }

        public override CustomerAliases Update(CustomerAliases entity)
        {
            _logger.LogDebug("Update started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
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
                _logger.LogDebug("Update finished.");
                var resultEntity = context.CustomerAliases.Include(a => a.Customer)
                                                          .Include(a => a.Restaurant)
                                                          .FirstOrDefault(a => a.Id == entity.Id);
                return _mapper.Map<CustomerAliases>(resultEntity);
            }
        }
    }
}
