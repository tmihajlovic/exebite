using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
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
        private readonly IMapper _exebiteMapper;

        public FoodController(IFoodService foodService, IMapper exebiteMapper)
        {
            _foodService = foodService;
            _exebiteMapper = exebiteMapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var foods = _exebiteMapper.Map<IEnumerable<FoodViewModel>>(_foodService.GetAllFoods());
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var food = _foodService.GetFoodById(id);
            if (food == null)
            {
                return NotFound();
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
                return NotFound();
            }

            _exebiteMapper.Map(model, currentFood);

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
