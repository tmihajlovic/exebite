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
                var locEntity = _mapper.Map<LocationEntity>(location);
                var resultEntity = context.Locations.Update(locEntity).Entity;
                context.SaveChanges();
                _logger.LogDebug("Insert finished.");
                return _mapper.Map<Location>(resultEntity);
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
                var locationEntity = _mapper.Map<LocationEntity>(location);
                context.Update(locationEntity);
                context.SaveChanges();
                _logger.LogDebug("Update finished.");
                var resultEntity = context.Locations.FirstOrDefault(l => l.Id == location.Id);
                return _mapper.Map<Location>(resultEntity);
            }
        }

        public override IList<Location> Query(LocationQueryModel queryModel)
        {
            _logger.LogDebug("Querying started.");
            if (queryModel == null)
            {
                _logger.LogError($"Argument {queryModel} is null");
                throw new ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Locations.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                _logger.LogDebug("Querying finished.");
                return _mapper.Map<IList<Location>>(results);
            }
        }
    }
}
