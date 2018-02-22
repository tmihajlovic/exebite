using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.Model;

namespace Exebite.Business
{
    public class LocationService : ILocationService
    {
        ILocationRepository _locationHandler;

        public LocationService(ILocationRepository locationHandler)
        {
            _locationHandler = locationHandler;
        }

        public List<Location> GetAllLocations()
        {
            return _locationHandler.Get().ToList();
        }

        public Location GetLocationById(int locationId)
        {
            return _locationHandler.GetByID(locationId);
        }

        public Location GetLocationByName(string name)
        {
            return _locationHandler.Get().SingleOrDefault(l => l.Name == name);
            
        }

        public void UpdateLocation(Location location)
        {
            _locationHandler.Update(location);
        }

        public void CreateNewLocation(Location location)
        {
            _locationHandler.Insert(location);
        }

        public void DeleteLocation(int locationId)
        {
            _locationHandler.Delete(locationId);
        }
    }
}
