using System.Linq;
using Exebite.API.Models;
using Exebite.Business;
using Exebite.DataAccess.AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/food")]
    [Authorize]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        private readonly IExebiteMapper _exebiteMapper;

        public FoodController(IFoodService foodService, IExebiteMapper exebiteMapper)
        {
            _foodService = foodService;
            _exebiteMapper = exebiteMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var foods = _foodService.GetAllFoods().Select(_exebiteMapper.Map<FoodViewModel>);
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var food = _foodService.GetFoodById(id);
            if (food == null)
            {
                return BadRequest();
            }

            return Ok(_exebiteMapper.Map<FoodViewModel>(food));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateFoodModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdFood = _foodService.CreateNewFood(_exebiteMapper.Map<Model.Food>(model));

            return Ok(new { createdFood.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateFoodModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentFood = _foodService.GetFoodById(id);
            if (currentFood == null)
            {
                return BadRequest();
            }

            currentFood.Name = model.Name;
            currentFood.Description = model.Description;
            currentFood.IsInactive = model.IsInactive;
            currentFood.Price = model.Price;
            currentFood.RestaurantId = model.RestaurantId;
            currentFood.Type = model.Type;

            var updatedFood = _foodService.UpdateFood(currentFood);
            return Ok(new { updatedFood.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _foodService.Delete(id);
            return NoContent();
        }
    }
}
