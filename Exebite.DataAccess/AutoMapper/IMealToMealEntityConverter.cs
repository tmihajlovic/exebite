using System;
using AutoMapper;
using Exebite.DataAccess.Entities;
using Exebite.DomainModel;

namespace Exebite.DataAccess.AutoMapper
{
    [Obsolete]
    public interface IMealToMealEntityConverter : ITypeConverter<Meal, MealEntity>
    {
    }
}