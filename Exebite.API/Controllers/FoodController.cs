using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/food")]
    [Authorize]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var foods = _mapper.Map<IEnumerable<FoodModel>>(_foodRepository.Get(0, int.MaxValue));
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var food = _foodRepository.GetByID(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FoodModel>(food));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateFoodModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdFood = _foodRepository.Insert(_mapper.Map<DomainModel.Food>(model));

            return Ok(new { createdFood.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateFoodModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentFood = _foodRepository.GetByID(id);
            if (currentFood == null)
            {
                return NotFound();
            }

            _mapper.Map(model, currentFood);
            var updatedFood = _foodRepository.Update(currentFood);
            return Ok(new { updatedFood.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _foodRepository.Delete(id);
            return NoContent();
        }

        [HttpGet("Query")]
        public IActionResult Query(FoodQueryModel query)
        {
            var foods = _foodRepository.Query(query);
            return Ok(_mapper.Map<IEnumerable<FoodModel>>(foods));
        }
    }
}
