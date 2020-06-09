using System;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Enums;

namespace Exebite.GoogleSheetAPI.Extensions
{
    /// <summary>
    /// Extension method for manipulating Enumeration classes.
    /// </summary>
    public static class EnumsExtensions
    {
        /// <summary>
        /// Get a String representation of FoodType enum.
        /// </summary>
        /// <param name="type">FoodType</param>
        /// <returns>string value of FoodType</returns>
        public static string ConvertToString(this MealType type)
        {
            switch (type)
            {
                case MealType.SIDE_DISH:
                    return "Prilog";
                case MealType.SALAD:
                    return "Salata";
                case MealType.DESSERT:
                    return "Desert";
                case MealType.SOUP:
                    return "Supa";
                case MealType.CONDIMENT:
                    return "Dodatak";
                case MealType.MAIN_COURSE:
                default:
                    return "Glavno jelo";
            }
        }

        /// <summary>
        /// Get a String representation of ESheetOwner enum. This data should match database information.
        /// </summary>
        /// <param name="owner">ESheetOwner</param>
        /// <returns>string value of ESheetOwner</returns>
        public static string ConvertToString(this ESheetOwner owner)
        {
            switch (owner)
            {
                case ESheetOwner.LIPA:
                    return "Kuhinjica pod Lipom";
                case ESheetOwner.INDEX_HOUSE:
                    return "Index House";
                case ESheetOwner.MIMAS:
                    return "Mima's";
                case ESheetOwner.KASA:
                    return "Kasa";
                case ESheetOwner.TOPLI_OBROK:
                    return "Topli Obrok";
                case ESheetOwner.SERPICA:
                default:
                    throw new ArgumentException("Chosen sheet owner does not exist.");
            }
        }

        /// <summary>
        /// Try to convert string value in sheets to a FoodType enum representation.
        /// </summary>
        /// <param name="value">string value of FoodType</param>
        /// <returns>FoodType enum</returns>
        public static MealType ConvertToMealType(this string value)
        {
            switch (value)
            {
                case "Prilog":
                    return MealType.SIDE_DISH;
                case "Salata":
                    return MealType.SALAD;
                case "Desert":
                    return MealType.DESSERT;
                case "Supa":
                    return MealType.SOUP;
                case "Dodatak":
                    return MealType.CONDIMENT;
                case "Glavno jelo":
                default:
                    return MealType.MAIN_COURSE;
            }
        }

        /// <summary>
        /// Convert price of the food to FoodType enum. Should only be used for restaurants that have free condiments.
        /// </summary>
        /// <param name="price">Price of the food.</param>
        /// <returns>FoodType</returns>
        public static MealType IndexHouseMealTypeFromPrice(this decimal price)
        {
            return price > 0m ? MealType.MAIN_COURSE : MealType.CONDIMENT;
        }
    }
}