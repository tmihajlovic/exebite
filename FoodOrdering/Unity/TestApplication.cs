namespace FoodOrdering.Unity
{
    public class TestApplication : IApplication
    {
        public void Run(string[] args)
        {

            #region Customer
            var loc = new Location
            {
                Name = "NameLoca",
                Address = "Adresa"

            };

            var cuustomer = new Customer
            {
                Name = "NameCustomera",
                Balance = 10,
                Location = loc
            };

            ICustomerHandler customerHandler = UnityConfig.Container.Resolve<ICustomerHandler>();
            customerHandler.Insert(cuustomer);

            //if(customerHandler.Get().ToList().Count < 1)
            //    Console.WriteLine("BAD: Customer Get");

            //if(customerHandler.GetByName("NameCustomera").Name != "NameCustomera")
            //    Console.WriteLine("BAD: Customer GetByName");

            //if (customerHandler.GetByID(1).Name != "NameCustomera")
            //    Console.WriteLine("BAD: Customer GetById");

            //customerHandler.Delete(1);
            #endregion

            #region Location
            var location = new Location()
            {
                Address = "Adresa",
                Name = "Lokacija"
            };

            ILocationHandler locationHandler = UnityConfig.Container.Resolve<ILocationHandler>();
            locationHandler.Insert(location);

            //if(locationHandler.Get().ToList().Count < 1)
            //    Console.WriteLine("BAD: Location Get");

            //if (locationHandler.GetByID(1).Name == "Lokacija")
            //    Console.WriteLine("BAD: Location Get");

            //locationHandler.Delete(1);
            #endregion

            #region Food

            var restaurant = new Restaurant()
            {
                Name = "Lipa"
            };
            var food = new Food
            {
                Name = "sarma",
                Price = 100,
                Type = FoodType.MAIN_COURSE,
                Restaurant = restaurant
            };

            IFoodHandler foodHandler = UnityConfig.Container.Resolve<IFoodHandler>();

            foodHandler.Insert(food);

            //if(foodHandler.Get().ToList().Count < 1)
            //    Console.WriteLine("BAD: Food Get");

            //if(foodHandler.GetByRestaurant(restaurant).ToList()[0].Name != "sarma")
            //    Console.WriteLine("BAD: Food GetByRestaurant");

            //if(foodHandler.GetByID(1).Name != "sarma")
            //    Console.WriteLine("BAD: Food GetByID");

            //foodHandler.Delete(1);
            #endregion

            #region Meal

            IMealHandler mealHandler = UnityConfig.Container.Resolve<IMealHandler>();

            var rest = new Restaurant
            {
                Name = "Lipa"
            };

            var foods = new List<Food>
            {
                new Food{Name = "Pilav", Price = 100, Type = FoodType.MAIN_COURSE, Restaurant = rest},
                new Food{Name = "Musaka", Price = 50, Type = FoodType.MAIN_COURSE, Restaurant = rest}
            };

            var meal = new Meal
            {
                Price = 100,
                Foods = foods
            };

            mealHandler.Insert(meal);

            //if(mealHandler.Get().ToList().Count < 1)
            //    Console.WriteLine("BAD: Meal Get");

            //if (mealHandler.GetByID(2).Price != 100)
            //    Console.WriteLine("BAD: Meal GetByID");

            //mealHandler.Delete(2);

            #endregion

            #region Order

            IOrderHandler orderHandler = UnityConfig.Container.Resolve<IOrderHandler>();

            var rest2 = new Restaurant
            {
                Name = "Extrafood"
            };

            var foods2 = new List<Food>
            {
                new Food{Name = "Pilav2", Price = 1002, Type = FoodType.MAIN_COURSE, Restaurant = rest2},
                new Food{Name = "Musaka2", Price = 502, Type = FoodType.MAIN_COURSE, Restaurant = rest2}
            };

            var meal2 = new Meal
            {
                Price = 1002,
                Foods = foods2
            };

            var loc2 = new Location
            {
                Name = "NameLoca2",
                Address = "Adresa2"

            };

            var customer2 = new Customer
            {
                Name = "NameCustomera2",
                Balance = 102,
                Location = loc2
            };

            var date = DateTime.Now.Date;
            var order = new Order
            {
                Meal = meal2,
                Customer = customer2,
                Date = date,
                Note = "Note",
                Price = 213
            };

            orderHandler.Insert(order);

            //if (orderHandler.Get().ToList().Count < 1)
            //    Console.WriteLine("BAD: Order Get");

            //if (orderHandler.GetByID(1).Price != 213)
            //    Console.WriteLine("BAD: Order GetByID");

            //if (orderHandler.GetOrdersForCustomer(cuustomer).ToList()[0].Price !=213)
            //    Console.WriteLine("BAD: Order GetOrdersForCustomer");

            //if (orderHandler.GetOrdersForDate(date).ToList()[0].Price != 213)
            //    Console.WriteLine("BAD: Order GetOrdersForDate");

            //orderHandler.Delete(1);

            #endregion

            #region Restaurant


            IRestaurantHandler restaurantHandler = UnityConfig.Container.Resolve<IRestaurantHandler>();

            var restaurant3 = new Restaurant
            {
                Name = "Name3"
            };

            restaurantHandler.Insert(restaurant3);

            //if(restaurantHandler.Get().ToList()[0].Name != "Name3")
            //    Console.WriteLine("BAD: Restaurant Get");

            //if (restaurantHandler.GetByID(0).Name != "Name3")
            //    Console.WriteLine("BAD: Restaurant GetByID");

            //if (restaurantHandler.GetByName("Name3") != null)
            //    Console.WriteLine("BAD: Restaurant Get");

            //restaurantHandler.Delete(1);

            #endregion
        }
    }
}
