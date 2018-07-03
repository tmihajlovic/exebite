using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/meal")]
    [Authorize]
    public class MealController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMapper _exebiteMapper;

        public MealController(IMealRepository mealRepository, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _mealRepository = mealRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var meals = _exebiteMapper.Map<IEnumerable<MealModel>>(_mealRepository.Get(0, int.MaxValue));
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var meal = _mealRepository.GetByID(id);
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

            var createdMeal = _mealRepository.Insert(_exebiteMapper.Map<DomainModel.Meal>(model));

            return Ok(new { createdMeal.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateMealModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentMeal = _mealRepository.GetByID(id);
            if (currentMeal == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentMeal);

            var updatedMeal = _mealRepository.Update(currentMeal);
            return Ok(new { updatedMeal.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _mealRepository.Delete(id);
            return NoContent();
        }
    }
}
