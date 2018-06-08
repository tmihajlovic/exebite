using System.Collections.Generic;
using System.Linq;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IExebiteMapper _exebiteMapper;

        public RestaurantController(IRestaurantService restaurantService, IExebiteMapper exebiteMapper)
        {
            _restaurantService = restaurantService;
            _exebiteMapper = exebiteMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var restaurants = _restaurantService.GetAllRestaurants().Select(_exebiteMapper.Map<RestaurantViewModel>);
            return Ok(restaurants);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var restaurant = _restaurantService.GetRestaurantById(id);
            if (restaurant == null)
            {
                return BadRequest();
            }

            return Ok(_exebiteMapper.Map<RestaurantViewModel>(restaurant));
        }

        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            var createdRestaurant = _restaurantService.CreateNewRestaurant(
                    new Model.Restaurant
                    {
                        Name = value,
                        DailyMenu = new List<Model.Food>(),
                        Foods = new List<Model.Food>()
                    });

            return Ok(createdRestaurant.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            var currentRestaurant = _restaurantService.GetRestaurantById(id);
            if (currentRestaurant == null)
            {
                return BadRequest();
            }

            currentRestaurant.Name = value;
            var updatedRestaurant = _restaurantService.UpdateRestaurant(currentRestaurant);

            return Ok(updatedRestaurant.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }
    }
}
