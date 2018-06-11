using System.Linq;
using AutoMapper;
using Exebite.DataAccess.AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DataAccess.Migrations;
using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public class LocationRepository : DatabaseRepository<Location, LocationEntity>, ILocationRepository
    {
        public LocationRepository(IFoodOrderingContextFactory factory, IMapper mapper)
            : base(factory, mapper)
        {
        }

        public override Location Insert(Location entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            using (var context = _factory.Create())
            {
                var locEntity = _mapper.Map<LocationEntity>(entity);
                var resultEntity = context.Locations.Update(locEntity).Entity;
                context.SaveChanges();
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
    }
}
