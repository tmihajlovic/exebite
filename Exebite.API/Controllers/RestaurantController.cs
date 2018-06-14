using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var restaurants = _mapper.Map<IEnumerable<RestaurantModel>>(_restaurantRepository.Get(0, int.MaxValue));
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var restaurant = _restaurantRepository.GetByID(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RestaurantModel>(restaurant));
        }

        [HttpPost]
        public IActionResult Post([FromBody]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var createdRestaurant = _restaurantRepository.Insert(new Model.Restaurant { Name = name });
            return Ok(createdRestaurant.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var currentRestaurant = _restaurantRepository.GetByID(id);
            if (currentRestaurant == null)
            {
                return NotFound();
            }

            currentRestaurant.Name = name;
            var updatedRestaurant = _restaurantRepository.Update(currentRestaurant);

            return Ok(updatedRestaurant.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _restaurantRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("Query")]
        public IActionResult Query(RestaurantQueryModel query)
        {
            var locations = _restaurantRepository.Query(query);
            return Ok(_mapper.Map<IEnumerable<RestaurantModel>>(locations));
        }
    }
}
