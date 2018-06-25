using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Exebite.DataAccess.Context;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;
using Microsoft.Extensions.Logging;

namespace Exebite.DataAccess.Repositories
{
    public class LocationRepository : DatabaseRepository<Location, LocationEntity, LocationQueryModel>, ILocationRepository
    {
        public LocationRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<LocationRepository> logger)
            : base(factory, mapper, logger)
        {
        }

        public override Location Insert(Location entity)
        {
            _logger.LogDebug("Insert started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locEntity = new LocationEntity
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Address = entity.Address
                };

                var createdEntity = context.Locations.Add(locEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Insert finished.");
                return _mapper.Map<Location>(createdEntity);
            }
        }

        public override Location Update(Location entity)
        {
            _logger.LogDebug("Update started.");
            if (entity == null)
            {
                _logger.LogError($"Argument {entity} is null");
                throw new ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locationEntity = context.Locations.Find(entity.Id);
                locationEntity.Name = entity.Name;
                locationEntity.Address = entity.Address;

                context.SaveChanges();
                _logger.LogDebug("Update finished.");
                return _mapper.Map<Location>(locationEntity);
            }
        }

        public override IList<Location> Query(LocationQueryModel queryModel)
        {
            _logger.LogDebug("Querying started.");
            if (queryModel == null)
            {
                _logger.LogError($"Argument {queryModel} is null");
                throw new ArgumentNullException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Locations.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                _logger.LogDebug("Querying by ", queryModel.ToString());
                var results = query.ToList();

                _logger.LogDebug("Querying finished.");
                return _mapper.Map<IList<Location>>(results);
            }
        }
    }
}
