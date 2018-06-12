using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.Model;

namespace Exebite.Business
{
    public class LocationService : ILocationService
    {
        private ILocationRepository _locationHandler;

        public LocationService(ILocationRepository locationHandler)
        {
            _locationHandler = locationHandler;
        }

        public IList<Location> GetLocations(int page, int size)
        {
            return _locationHandler.Get(page, size);
        }

        public Location GetLocationById(int locationId)
        {
            return _locationHandler.GetByID(locationId);
        }

        public Location GetLocationByName(string name)
        {
            if (name == string.Empty)
            {
                throw new System.ArgumentException("Name cant be empty string");
            }

            return _locationHandler.Get(0, int.MaxValue).SingleOrDefault(l => l.Name == name);
        }

        public Location UpdateLocation(Location location)
        {
            return _locationHandler.Update(location);
        }

        public Location CreateNewLocation(Location location)
        {
            if (location == null)
            {
                throw new System.ArgumentNullException(nameof(location));
            }

            return _locationHandler.Insert(location);
        }

        public void DeleteLocation(int locationId)
        {
            var location = _locationHandler.GetByID(locationId);
            if (location != null)
            {
                _locationHandler.Delete(locationId);
            }
        }
    }
}
