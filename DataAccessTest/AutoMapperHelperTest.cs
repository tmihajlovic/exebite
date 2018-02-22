using Exebite.DataAccess;
using Exebite.DataAccess.Entities;
using Exebite.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataAccessTest
{
    [TestClass]
    public class AutoMapperHelperTest
    {

        [TestMethod]
        public void GetMappedValue_Customer_EqualValues()
        {
            var customer1 = new CustomerEntity()
            {
                Balance = 10,
                Name = "CustomerName",
                Id = 1
            };

            var customer2 = AutoMapperHelper.Instance.GetMappedValue<Customer>(customer1);

            Assert.IsTrue(customer1.Balance == customer2.Balance);
            Assert.IsTrue(customer1.Name == customer2.Name);
            Assert.IsTrue(customer1.Id == customer2.Id);
        }

        [TestMethod]
        public void GetMappedValue_CustomerEntity_EqualValues()
        {
            var customer1 = new Customer()
            {
                Balance = 10,
                Name = "CustomerName",
                Id = 1
            };

            var customer2 = AutoMapperHelper.Instance.GetMappedValue<CustomerEntity>(customer1);

            Assert.IsTrue(customer1.Balance == customer2.Balance);
            Assert.IsTrue(customer1.Name == customer2.Name);
            Assert.IsTrue(customer1.Id == customer2.Id);
        }


        [TestMethod]
        public void GetMappedValue_Location_EqualValues()
        {
            var location1 = new LocationEntity()
            {
                Name = "LocationName",
                Id = 2,
                Address ="Adresa"
            };

            var location2 = AutoMapperHelper.Instance.GetMappedValue<Location>(location1);

            Assert.IsTrue(location1.Address == location2.Address);
            Assert.IsTrue(location1.Name == location2.Name);
            Assert.IsTrue(location1.Id == location2.Id);
        }

        [TestMethod]
        public void GetMappedValue_LocationEntity_EqualValues()
        {
            var location1 = new Location()
            {
                Name = "LocationName",
                Id = 2,
                Address = "Adresa"
            };

            var location2 = AutoMapperHelper.Instance.GetMappedValue<LocationEntity>(location1);

            Assert.IsTrue(location1.Address == location2.Address);
            Assert.IsTrue(location1.Name == location2.Name);
            Assert.IsTrue(location1.Id == location2.Id);
        }

        [TestMethod]
        public void GetMappedValue_Restaurant_EqualValues()
        {
            var restaurant1 = new RestaurantEntity()
            {
                Name = "RestaurantName",
                Id = 3,
            };

            var restaurant2 = AutoMapperHelper.Instance.GetMappedValue<Restaurant>(restaurant1);
            
            Assert.IsTrue(restaurant1.Name == restaurant2.Name);
            Assert.IsTrue(restaurant1.Id == restaurant2.Id);
        }

        [TestMethod]
        public void GetMappedValue_RestaurantEntity_EqualValues()
        {
            var restaurant1 = new Restaurant()
            {
                Name = "RestaurantName",
                Id = 3,
            };

            var restaurant2 = AutoMapperHelper.Instance.GetMappedValue<RestaurantEntity>(restaurant1);

            Assert.IsTrue(restaurant1.Name == restaurant2.Name);
            Assert.IsTrue(restaurant1.Id == restaurant2.Id);
        }

        [TestMethod]
        public void GetMappedValue_Meal_EqualValues()
        {
            var meal1 = new MealEntity()
            {
                Id = 4,
                Price = 25
            };

            var meal2 = AutoMapperHelper.Instance.GetMappedValue<Meal>(meal1);

            Assert.IsTrue(meal1.Price == meal2.Price);
            Assert.IsTrue(meal1.Id == meal2.Id);
        }

        [TestMethod]
        public void GetMappedValue_MealEntity_EqualValues()
        {
            var meal1 = new Meal()
            {
                Id = 4,
                Price = 25
            };

            var meal2 = AutoMapperHelper.Instance.GetMappedValue<MealEntity>(meal1);

            Assert.IsTrue(meal1.Price == meal2.Price);
            Assert.IsTrue(meal1.Id == meal2.Id);
        }

        [TestMethod]
        public void GetMappedValue_Order_EqualValues()
        {
            var order1 = new OrderEntity()
            {
                Id = 5,
                Price = 30,
                Date = DateTime.Now,
                Note = "Note"
            };

            var order2 = AutoMapperHelper.Instance.GetMappedValue<Order>(order1);

            Assert.IsTrue(order1.Id == order2.Id);
            Assert.IsTrue(order1.Price == order2.Price);
            Assert.IsTrue(order1.Date == order2.Date);
            Assert.IsTrue(order1.Note == order2.Note);
        }

        [TestMethod]
        public void GetMappedValue_OrderEntity_EqualValues()
        {
            var order1 = new Order()
            {
                Id = 5,
                Price = 30,
                Date = DateTime.Now,
                Note = "Note"
            };

            var order2 = AutoMapperHelper.Instance.GetMappedValue<OrderEntity>(order1);

            Assert.IsTrue(order1.Id == order2.Id);
            Assert.IsTrue(order1.Price == order2.Price);
            Assert.IsTrue(order1.Date == order2.Date);
            Assert.IsTrue(order1.Note == order2.Note);
        }

        [TestMethod]
        public void GetMappedValue_Food_EqualValues()
        {
            var food1 = new FoodEntity()
            {
                Id = 10,
                Price = 40,
                Name = "Food",
                Type = FoodType.MAIN_COURSE
            };

            var food2 = AutoMapperHelper.Instance.GetMappedValue<Food>(food1);

            Assert.IsTrue(food1.Id == food2.Id);
            Assert.IsTrue(food1.Price == food2.Price);
            Assert.IsTrue(food1.Name == food2.Name);
            Assert.IsTrue(food1.Type == food2.Type);
        }

        [TestMethod]
        public void GetMappedValue_FoodEntity_EqualValues()
        {
            var food1 = new Food()
            {
                Id = 10,
                Price = 40,
                Name = "Food",
                Type = FoodType.MAIN_COURSE
            };

            var food2 = AutoMapperHelper.Instance.GetMappedValue<FoodEntity>(food1);

            Assert.IsTrue(food1.Id == food2.Id);
            Assert.IsTrue(food1.Price == food2.Price);
            Assert.IsTrue(food1.Name == food2.Name);
            Assert.IsTrue(food1.Type == food2.Type);
        }

        [TestMethod]
        public void GetMappedValue_Recipe_EqualValues()
        {
            var recipe1 = new RecipeEntity()
            {
                Id = 10
            };

            var recipe2 = AutoMapperHelper.Instance.GetMappedValue<Recipe>(recipe1);

            Assert.IsTrue(recipe1.Id == recipe2.Id);
        }

        [TestMethod]
        public void GetMappedValue_RecipeEntity_EqualValues()
        {
            var recipe1 = new Recipe()
            {
                Id = 10
            };

            var recipe2 = AutoMapperHelper.Instance.GetMappedValue<RecipeEntity>(recipe1);

            Assert.IsTrue(recipe1.Id == recipe2.Id);
        }

    }
}
