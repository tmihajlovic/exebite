using System.Collections.Generic;
using AutoMapper;
using Exebite.API.Models;
using Exebite.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exebite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/recipe")]
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _exebiteMapper;

        public RecipeController(IRecipeService recipeService, IMapper exebiteMapper)
        {
            _exebiteMapper = exebiteMapper;
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var recipes = _exebiteMapper.Map<IEnumerable<RecipeModel>>(_recipeService.Get(0, int.MaxValue));
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var recipe = _recipeService.GetById(id);
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

            var createdRecipe = _recipeService.Create(_exebiteMapper.Map<Model.Recipe>(model));
            return Ok(new { createdRecipe.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateRecipeModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var currentRecipe = _recipeService.GetById(id);
            if (currentRecipe == null)
            {
                return NotFound();
            }

            _exebiteMapper.Map(model, currentRecipe);
            var updatedRecipe = _recipeService.Update(currentRecipe);
            return Ok(new { updatedRecipe.Id });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _recipeService.Delete(id);
            return NoContent();
        }
    }
}
