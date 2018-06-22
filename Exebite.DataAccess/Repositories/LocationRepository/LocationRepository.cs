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

        public override Location Insert(Location location)
        {
            _logger.LogDebug("Insert started.");
            if (location == null)
            {
                _logger.LogError($"Argument {location} is null");
                throw new ArgumentNullException(nameof(location));
            }

            using (var context = _factory.Create())
            {
                var locEntity = new LocationEntity
                {
                    Id = location.Id,
                    Name = location.Name,
                    Address = location.Address
                };

                var createdEntity = context.Locations.Add(locEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Insert finished.");
                return _mapper.Map<Location>(createdEntity);
            }
        }

        public override Location Update(Location location)
        {
            _logger.LogDebug("Update started.");
            if (location == null)
            {
                _logger.LogError($"Argument {location} is null");
                throw new ArgumentNullException(nameof(location));
            }

            using (var context = _factory.Create())
            {
                var locationEntity = context.Locations.Find(location.Id);
                locationEntity.Name = location.Name;
                locationEntity.Address = location.Address;

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
