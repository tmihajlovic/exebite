using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/recipe")]
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _exebiteMapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var recipes = _exebiteMapper.Map<IEnumerable<RecipeModel>>(_recipeRepository.Get(0, int.MaxValue));
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _recipeRepository.GetByID(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(_exebiteMapper.Map<RecipeModel>(recipe));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateRecipeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var createdRecipe = _recipeRepository.Insert(_exebiteMapper.Map<DomainModel.Recipe>(model));
            return Ok(new { createdRecipe.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateRecipeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentRecipe = _recipeRepository.GetByID(id);
            if (currentRecipe == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentRecipe);
            var updatedRecipe = _recipeRepository.Update(currentRecipe);
            return Ok(new { updatedRecipe.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipeRepository.Delete(id);
            return NoContent();
        }
    }
}
