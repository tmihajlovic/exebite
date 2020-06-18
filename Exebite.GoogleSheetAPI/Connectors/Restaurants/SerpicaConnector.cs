using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Connectors.Restaurants.Base;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Exebite.GoogleSheetAPI.SheetExtractor;
using Newtonsoft.Json;

namespace Exebite.GoogleSheetAPI.Connectors.Restaurants
{
    public sealed class SerpicaConnector : RestaurantConnector, ISerpicaConnector
    {
        public SerpicaConnector(
            IGoogleSheetExtractor googleSheetService,
            IGoogleSpreadsheetIdFactory googleSSIdFactory,
            IRestaurantQueryRepository restaurantQueryRepository)
            : base(googleSheetService, restaurantQueryRepository, RestaurantConstants.SERPICA_NAME)
        {
            SheetId = googleSSIdFactory.GetSheetId(Enums.ESheetOwner.SERPICA);
            DailyMenuSheet = GetLocalMonthName(DateTime.Now.Month) + DateTime.Now.Year;
        }

        public override void WriteMenu(List<Meal> foods)
        {
            throw new NotImplementedException();
        }

        public List<Meal> GetDailyMenu()
        {
            var allFood = new List<Meal>();
            allFood.AddRange(GetDailyFromUrlAsync().Result);
            return allFood;
        }

        public async Task<IEnumerable<Meal>> GetDailyFromUrlAsync()
        {
            string dailyMenuString = string.Empty;
            List<Meal> dailyMenu = new List<Meal>();

            var loginContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("brtelefona", Properties.Resources.SERPICA_USERNAME),
                new KeyValuePair<string, string>("sifra", Properties.Resources.SERPICA_PASSWORD),
                new KeyValuePair<string, string>("zapamtime", "false")
            });

            var dailyMenuContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("datum", DateTime.Today.ToString("yyyy-MM-dd"))
            });

            using (var client = new HttpClient())
            {
                try
                {
                    var responseLogin = await client.PostAsync(Properties.Resources.SERPICA_LOGIN_URL, loginContent).ConfigureAwait(false);
                    var responseMenu = await client.PostAsync(Properties.Resources.SERPICA_DAILY_MENU_URL, dailyMenuContent).ConfigureAwait(false);
                    dailyMenuString = responseMenu.Content.ReadAsStringAsync().Result;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }

            var meals = JsonConvert.DeserializeObject<List<List<string>>>(dailyMenuString);

            foreach (var meal in meals)
            {
                if (meal[7] != "0")
                {
                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1],
                        Description = meal[2],
                        Price = int.Parse(meal[7]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });
                }
                else if (meal[8] == "6")
                {
                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1] + " - Jedan paket",
                        Description = meal[2],
                        Price = int.Parse(meal[5]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });

                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1] + " - Dva paketa",
                        Description = meal[2],
                        Price = int.Parse(meal[6]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });
                }
                else
                {
                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1] + " - Mala porcija",
                        Description = meal[2],
                        Price = int.Parse(meal[5]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });

                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1] + " - Velika porcija",
                        Description = meal[2],
                        Price = int.Parse(meal[6]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });

                    dailyMenu.Add(new Meal()
                    {
                        Name = meal[1] + " - XL porcija",
                        Description = meal[2],
                        Price = int.Parse(meal[9]),
                        Type = (int)GetMealCategoryName(int.Parse(meal[8])),
                        Restaurant = Restaurant
                    });
                }
            }

            return dailyMenu;
        }

        private MealType GetMealCategoryName(int index)
        {
            switch (index)
            {
                case 5:
                case 8:
                case 10:
                    return MealType.SALAD;
                case 6:
                    return MealType.SIDE_DISH;
                case 11:
                    return MealType.DESSERT;
                default:
                    return MealType.MAIN_COURSE;
            }
        }
    }
}
