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
        public static string ConvertToString(this FoodType type)
        {
            switch (type)
            {
                case FoodType.SIDE_DISH:
                    return "Prilog";
                case FoodType.SALAD:
                    return "Salata";
                case FoodType.DESERT:
                    return "Desert";
                case FoodType.SOUP:
                    return "Supa";
                case FoodType.CONDIMENTS:
                    return "Dodatak";
                case FoodType.MAIN_COURSE:
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
                case ESheetOwner.DE_PAPAJ:
                    return "De Papaj";
                case ESheetOwner.MIMAS:
                    return "Mima's";
                case ESheetOwner.KASA:
                    return "Kasa";
                case ESheetOwner.SERPICA:
                case ESheetOwner.HEDONE:
                case ESheetOwner.TEGLAS:
                    throw new ArgumentException("Obsolete.");
                default:
                    throw new ArgumentException("Chosen sheet owner does not exist.");
            }
        }

        /// <summary>
        /// Try to convert string value in sheets to a FoodType enum representation.
        /// </summary>
        /// <param name="value">string value of FoodType</param>
        /// <returns>FoodType enum</returns>
        public static FoodType ConvertToFoodType(this string value)
        {
            switch (value)
            {
                case "Prilog":
                    return FoodType.SIDE_DISH;
                case "Salata":
                    return FoodType.SALAD;
                case "Desert":
                    return FoodType.DESERT;
                case "Supa":
                    return FoodType.SOUP;
                case "Dodatak":
                    return FoodType.CONDIMENTS;
                case "Glavno jelo":
                default:
                    return FoodType.MAIN_COURSE;
            }
        }

        /// <summary>
        /// Convert price of the food to FoodType enum. Should only be used for restaurants that have free condiments.
        /// </summary>
        /// <param name="price">Price of the food.</param>
        /// <returns>FoodType</returns>
        public static FoodType IndexHouseFoodTypeFromPrice(this decimal price)
        {
            return price > 0m ? FoodType.MAIN_COURSE : FoodType.CONDIMENTS;
        }
    }
}