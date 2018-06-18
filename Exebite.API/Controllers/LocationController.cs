using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/location")]
    [Authorize]
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = _mapper.Map<IEnumerable<LocationModel>>(_locationRepository.Get(0, int.MaxValue));
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var location = _locationRepository.GetByID(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<LocationModel>(location));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateLocationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdLocation = _locationRepository.Insert(_mapper.Map<DomainModel.Location>(model));

            return Ok(new { createdLocation.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateLocationModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentLocation = _locationRepository.GetByID(id);
            if (currentLocation == null)
            {
                return NotFound();
            }

            _mapper.Map(model, currentLocation);

            var updatedLocation = _locationRepository.Update(currentLocation);
            return Ok(new { updatedLocation.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _locationRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("Query")]
        public IActionResult Query(LocationQueryModel query)
        {
            var locations = _locationRepository.Query(query);
            return Ok(_mapper.Map<IEnumerable<LocationModel>>(locations));
        }
    }
}
