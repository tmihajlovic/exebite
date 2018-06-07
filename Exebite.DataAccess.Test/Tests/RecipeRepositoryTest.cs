﻿using System;
using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess.Migrations;
using Exebite.DataAccess.Repositories;
using Exebite.DataAccess.Test.InMemoryDB;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Unity;
// using Unity.Resolution;

namespace Exebite.DataAccess.Test.Tests
{
    [TestClass]
    public class RecipeRepositoryTest
    {
        private static IFoodOrderingContextFactory _factory;
        private static IRecipeRepository _recepieRepository;
       // private static IUnityContainer _container;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _factory = new InMemoryDBFactory();
            // _container = new UnityContainer();
            // Unity.UnityConfig.RegisterTypes(_container);
           // _recepieRepository = _container.Resolve<IRecipeRepository>(new ParameterOverride("factory", _factory));
            InMemorySeed.Seed(_factory);
        }

        [TestMethod]
        public void GetAllRecepies()
        {
            var result = _recepieRepository.GetAll().ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void GetRecipeById()
        {
            var result = _recepieRepository.GetByID(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetRecipeById_NonExisting()
        {
            var result = _recepieRepository.GetByID(0);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetRecipesForMainCourse()
        {
            using (var context = _factory.Create())
            {
                var mainCourseEntity = context.Foods.Find(1);
                var mainCourse = AutoMapperHelper.Instance.GetMappedValue<Food>(mainCourseEntity, context);
                var result = _recepieRepository.GetRecipesForMainCourse(mainCourse);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void GetRecipesForMainCourse_NoSideDishes()
        {
            using (var context = _factory.Create())
            {
                var mainCourseEntity = context.Foods.Find(3);
                var mainCourse = AutoMapperHelper.Instance.GetMappedValue<Food>(mainCourseEntity, context);
                var result = _recepieRepository.GetRecipesForMainCourse(mainCourse);
                Assert.AreEqual(result.Count, 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRecipesForMainCourse_MainCourseIsNull()
        {
            _recepieRepository.GetRecipesForMainCourse(null);
        }

        [TestMethod]
        public void GetRecipesForFood()
        {
            using (var context = _factory.Create())
            {
                var foodEntity = context.Foods.FirstOrDefault(f => f.Type == FoodType.CONDIMENTS);
                var food = AutoMapperHelper.Instance.GetMappedValue<Food>(foodEntity, context);
                var result = _recepieRepository.GetRecipesForFood(food);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void GetRecipesForFood_NoRecipes()
        {
            using (var context = _factory.Create())
            {
                var foodEntity = context.Foods.First(f => f.Type == FoodType.SALAD);
                var food = AutoMapperHelper.Instance.GetMappedValue<Food>(foodEntity, context);
                var result = _recepieRepository.GetRecipesForFood(food);
                Assert.AreEqual(result.Count, 0);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRecipesForFood_FoodIsNull()
        {
            _recepieRepository.GetRecipesForFood(null);
        }

        [TestMethod]
        public void InsertRecipe()
        {
            using (var context = _factory.Create())
            {
                var restaurantEntity = context.Restaurants.Find(1);
                var restaurant = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurantEntity, context);
                var foodEntity = context.Foods.First();
                var food = AutoMapperHelper.Instance.GetMappedValue<Food>(foodEntity, context);
                var condamentEntity = context.Foods.FirstOrDefault(f => f.Type == FoodType.CONDIMENTS);
                var condament = AutoMapperHelper.Instance.GetMappedValue<Food>(condamentEntity, context);
                Recipe newRecipe = new Recipe
                {
                    MainCourse = food,
                    Restaurant = restaurant,
                    SideDish = new List<Food>
                       {
                           condament
                       }
                };
                var result = _recepieRepository.Insert(newRecipe);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertRecipe_IsNull()
        {
            _recepieRepository.Insert(null);
        }

        [TestMethod]
        public void UpdateRecipe()
        {
            using (var context = _factory.Create())
            {
                var recipieEntity = context.Recipes.Find(1);
                var recipie = AutoMapperHelper.Instance.GetMappedValue<Recipe>(recipieEntity, context);
                var newFoodEntity = context.Foods.FirstOrDefault(f => f.Type == FoodType.SALAD);
                var newFood = AutoMapperHelper.Instance.GetMappedValue<Food>(newFoodEntity, context);
                recipie.SideDish.Add(newFood);
                var result = _recepieRepository.Update(recipie);
                Assert.AreEqual(result.SideDish.Count, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateRecipe_IsNull()
        {
            _recepieRepository.Update(null);
        }

        [TestMethod]
        public void DeleteRecipe()
        {
            using (var context = _factory.Create())
            {
                _recepieRepository.Delete(2);
                var result = _recepieRepository.GetByID(2);
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public void DeleteRecipe_NonExisting()
        {
            _recepieRepository.Delete(0);
        }
    }
}
