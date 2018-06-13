using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.Model;

namespace Exebite.DataAccess.AutoMapper
{
    public interface IRecipeToRecipeEntityConverter : ITypeConverter<Recipe, RecipeEntity>
    {
    }
}
