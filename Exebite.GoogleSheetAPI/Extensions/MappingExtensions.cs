﻿using System;
using System.Collections.Generic;

namespace Exebite.GoogleSheetAPI.Extensions
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Parse and map string value of a Customer Name into Location name.
        /// </summary>
        /// <param name="name">String value of Customer Name.</param>
        /// <returns>Location object.</returns>
        public static string MapLocationName(this string name)
        {
            return name.EndsWith("JD")
                ? "Execom JD"
                : name.EndsWith("MM")
                    ? "Execom MM"
                    : "Execom VS";
        }

        /// <summary>
        /// Try to extract and convert value from the sheet onto the object. If it fails, it will return the callback value.
        /// This method makes sure there's always a value, even if an exception occurs.
        /// </summary>
        /// <typeparam name="T">Data Type of the cell.</typeparam>
        /// <param name="objectList">List of objects.</param>
        /// <param name="index">Index to access.</param>
        /// <param name="fallback">Fallback value to return in case of an error.</param>
        /// <returns>T</returns>
        public static T ExtractCell<T>(this IList<object> objectList, int index, T fallback)
        {
            T retVal;

            try
            {
                retVal = (T)Convert.ChangeType(objectList[index], typeof(T));

                if (retVal is string a && string.IsNullOrWhiteSpace(a))
                {
                    retVal = fallback;
                }
            }
            catch (Exception)
            {
                retVal = fallback;
            }

            return retVal;
        }
    }
}