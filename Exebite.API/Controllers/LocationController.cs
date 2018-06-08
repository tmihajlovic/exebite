using System.Linq;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/location")]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IExebiteMapper _exebiteMapper;

        public LocationController(ILocationService locationService, IExebiteMapper exebiteMapper)
        {
            _locationService = locationService;
            _exebiteMapper = exebiteMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = _locationService.GetAllLocations().Select(_exebiteMapper.Map<LocationViewModel>);
            return Ok(locations);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null)
            {
                return BadRequest();
            }

            return Ok(_exebiteMapper.Map<LocationViewModel>(location));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdLocation = _locationService.CreateNewLocation(_exebiteMapper.Map<Model.Location>(model));

            return Ok(createdLocation.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateLocationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentLocation = _locationService.GetLocationById(id);
            if (currentLocation == null)
            {
                return BadRequest();
            }

            currentLocation.Name = model.Name;
            currentLocation.Address = model.Address;

            var updatedLocation = _locationService.UpdateLocation(currentLocation);
            return Ok(updatedLocation.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _locationService.DeleteLocation(id);
            return NoContent();
        }
    }
}
