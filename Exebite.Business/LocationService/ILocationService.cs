using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.Business
{
    public interface ILocationService
    {
        /// <summary>
        /// Get all location
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns>List of locations</returns>
        IList<Location> GetLocations(int page, int size);

        /// <summary>
        /// Get <see cref="Location"/> by Id
        /// </summary>
        /// <param name="locationId">Id of location</param>
        /// <returns>location with given Id</returns>
        Location GetLocationById(int locationId);

        /// <summary>
        /// Get <see cref="Location"/> by name
        /// </summary>
        /// <param name="name">name of location</param>
        /// <returns>Location with given name</returns>
        Location GetLocationByName(string name);

        /// <summary>
        /// Create new location
        /// </summary>
        /// <param name="location"><see cref="Location"/>to be created</param>
        /// <returns>New location from database</returns>
        Location CreateNewLocation(Location location);

        /// <summary>
        /// Update location info
        /// </summary>
        /// <param name="location"><see cref="Location"/> with new info</param>
        /// <returns>Updated locatoin form database</returns>
        Location UpdateLocation(Location location);

        /// <summary>
        /// Delete location from database
        /// </summary>
        /// <param name="locationId">Id of location to be deleted</param>
        void DeleteLocation(int locationId);
    }
}
