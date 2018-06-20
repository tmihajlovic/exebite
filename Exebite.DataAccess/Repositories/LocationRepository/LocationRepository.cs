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
        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(IFoodOrderingContextFactory factory, IMapper mapper, ILogger<LocationRepository> logger)
            : base(factory, mapper)
        {
            _logger = logger;
        }

        public override Location Insert(Location location)
        {
            _logger.LogDebug("Insert started.");
            if (location == null)
            {
                _logger.LogError($"Argument {location} is null");
                throw new System.ArgumentNullException(nameof(location));
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

        public override Location Update(Location entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locationEntity = _mapper.Map<LocationEntity>(entity);
                context.Update(locationEntity);
                context.SaveChanges();
                var resultEntity = context.Locations.FirstOrDefault(l => l.Id == entity.Id);
                return _mapper.Map<Location>(resultEntity);
            }
        }

        public override IList<Location> Query(LocationQueryModel queryModel)
        {
            if (queryModel == null)
            {
                throw new System.ArgumentException("queryModel can't be null");
            }

            using (var context = _factory.Create())
            {
                var query = context.Locations.AsQueryable();

                if (queryModel.Id != null)
                {
                    query = query.Where(x => x.Id == queryModel.Id.Value);
                }

                var results = query.ToList();
                return _mapper.Map<IList<Location>>(results);
            }
        }
    }
}
