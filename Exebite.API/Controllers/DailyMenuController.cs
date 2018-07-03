using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/dailymenu")]
    [Authorize]
    public class DailyMenuController : ControllerBase
    {
        private readonly IDailyMenuRepository _dailyMenuRepository;
        private readonly IMapper _exebiteMapper;

        public DailyMenuController(IDailyMenuRepository dailyMenuRepository, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _dailyMenuRepository = dailyMenuRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var meals = _exebiteMapper.Map<IEnumerable<DailyMenuModel>>(_dailyMenuRepository.Get(0, int.MaxValue));
            return Ok(meals);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var meal = _dailyMenuRepository.GetByID(id);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(_exebiteMapper.Map<DailyMenuModel>(meal));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateDailyMenuModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdDailyMenu = _dailyMenuRepository.Insert(_exebiteMapper.Map<DomainModel.DailyMenu>(model));

            return Ok(new { createdDailyMenu.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateDailyMenuModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentDailyMenu = _dailyMenuRepository.GetByID(id);
            if (currentDailyMenu == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentDailyMenu);

            var updatedMeal = _dailyMenuRepository.Update(currentDailyMenu);
            return Ok(new { updatedMeal.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dailyMenuRepository.Delete(id);
            return NoContent();
        }
    }
}
