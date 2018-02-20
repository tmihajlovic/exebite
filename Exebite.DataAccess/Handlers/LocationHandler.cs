using System.Collections.Generic;
using System.Data.Entity;
using Exebite.DataAccess.Context;
using Exebite.Model;

namespace Exebite.DataAccess.Handlers
{
    public class LocationHandler : ILocationHandler
    {
        IFoodOrderingContextFactory _factory;
        public LocationHandler(IFoodOrderingContextFactory factory)
        {
            this._factory = factory;
        }

        public void Delete(int Id)
        {
            using (var context = _factory.Create())
            {
                var loc = context.Locations.Find(Id);
                context.Locations.Remove(loc);
            }
        }

        public IEnumerable<Location> Get()
        {
            using (var context = _factory.Create())
            {
                var locationEntities = new List<Location>();

                foreach (var location in context.Locations)
                {
                    var locModel = AutoMapperHelper.Instance.GetMappedValue<Location>(location);
                    locationEntities.Add(locModel);
                }

                return locationEntities;
            }
        }

        public Location GetByID(int Id)
        {
            using (var context = _factory.Create())
            {
                var loc = context.Locations.Find(Id);
                var locModel = AutoMapperHelper.Instance.GetMappedValue<Location>(loc);
                return locModel;
            }
        }

        public void Insert(Location entity)
        {
            using (var context = _factory.Create())
            {
                var locEntity = AutoMapperHelper.Instance.GetMappedValue<LocationEntity>(entity);
                context.Locations.Add(locEntity);
            }
        }
        
        public void Update(Location entity)
        {
            using (var context = _factory.Create())
            {
                var locationEntity = AutoMapperHelper.Instance.GetMappedValue<LocationEntity>(entity);
                context.Entry(locationEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
