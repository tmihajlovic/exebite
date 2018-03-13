using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
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

        public List<Location> GetAllLocations()
        {
            return _locationHandler.GetAll().ToList();
        }

        public Location GetLocationById(int locationId)
        {
            return _locationHandler.GetByID(locationId);
        }

        public Location GetLocationByName(string name)
        {
            return _locationHandler.GetAll().SingleOrDefault(l => l.Name == name);
        }

        public Location UpdateLocation(Location location)
        {
            return _locationHandler.Update(location);
        }

        public Location CreateNewLocation(Location location)
        {
            return _locationHandler.Insert(location);
        }

        public void DeleteLocation(int locationId)
        {
            _locationHandler.Delete(locationId);
        }
    }
}
