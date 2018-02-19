using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business
{
    public interface ILocationService
    {
        List<Location> GetAllLocations();
        Location GetLocationById(int locationId);
        Location GetLocationByName(string name);
        void CreateNewLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int locationId);
    }
}
