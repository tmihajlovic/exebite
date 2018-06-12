using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
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
        private readonly IMapper _exebiteMapper;

        public LocationController(ILocationService locationService, IMapper exebiteMapper)
        {
            _locationService = locationService;
            _exebiteMapper = exebiteMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = _exebiteMapper.Map<IEnumerable<LocationModel>>(_locationService.GetLocations(0, int.MaxValue));
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var location = _locationService.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(_exebiteMapper.Map<LocationModel>(location));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdLocation = _locationService.CreateNewLocation(_exebiteMapper.Map<Model.Location>(model));

            return Ok(new { createdLocation.Id });
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
                return NotFound();
            }

            _exebiteMapper.Map(model, currentLocation);

            var updatedLocation = _locationService.UpdateLocation(currentLocation);
            return Ok(new { updatedLocation.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _locationService.DeleteLocation(id);
            return NoContent();
        }
    }
}
