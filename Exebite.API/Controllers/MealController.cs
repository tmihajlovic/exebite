using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/meal")]
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMapper _exebiteMapper;

        public MealController(IMealService mealService, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _mealService = mealService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var meals = _exebiteMapper.Map<IEnumerable<MealModel>>(_mealService.Get(0, int.MaxValue));
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var meal = _mealService.GetById(id);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(_exebiteMapper.Map<MealModel>(meal));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateMealModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdMeal = _mealService.Create(_exebiteMapper.Map<Model.Meal>(model));

            return Ok(new { createdMeal.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateMealModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentMeal = _mealService.GetById(id);
            if (currentMeal == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentMeal);

            var updatedMeal = _mealService.Update(currentMeal);
            return Ok(new { updatedMeal.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _mealService.Delete(id);
            return NoContent();
        }
    }
}
