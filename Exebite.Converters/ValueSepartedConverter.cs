using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Exebite.Converters.Attributes;
using Exebite.Converters.Delimiters;

namespace Exebite.Converters
{
    public class ValueSepartedConverter : IValueSepartedConverter
    {
        public string Serialize<T>(IEnumerable<T> input, Delimiter delimiter) where T : class
        {
            var properties = typeof(T).GetProperties();
            if (properties.Length == 0)
            {
                return string.Empty;
            }

            // get headers
            var header = properties
                .Select(GetHeaderName)
                .Aggregate((a, b) => a + delimiter.Value + b);

            //create lines out of values
            var csvLines = input.Select(item => item.GetType()
                                .GetProperties()
                                .Select(property => GetPropertyValue(item, property.Name))
                                .Aggregate((a, b) => a + delimiter.Value + b));

            var body = new StringBuilder();
            foreach (var line in csvLines)
            {
                body.AppendLine(line);
            }

            //join header and body
            return string.Join(Environment.NewLine, header, body);
        }

        /// <summary>
        /// Deserializes the value separated file and maps output to collection of objects.
        /// </summary>
        /// <typeparam name="T">Type of the parameter</typeparam>
        /// <param name="inputLines">Lines to be split</param>
        /// <param name="delimiter">Delimiter of the values</param>
        /// <returns></returns>
        public IEnumerable<T> Deserialize<T>(string[] inputLines, Delimiter delimiter) where T : new()
        {
            var header = inputLines[0].Split(new[] { delimiter.Value }, StringSplitOptions.None);
            var body = inputLines.Skip(1);
            return new List<T>(body.Select(line =>
                                            CreateObjectFromValues<T>(line.Split(new[] { delimiter.Value }, StringSplitOptions.None), header)));
        }

        /// <summary>
        /// Get value separated header name for specified property.
        /// </summary>
        /// <param name="propertyInfo">Info of the property.</param>
        /// <returns>If header attribute is set returns value of it, otherwise property info name value.</returns>
        private string GetHeaderName(PropertyInfo propertyInfo)
        {
            if (Array.Find(propertyInfo.GetCustomAttributes(true), x => x is HeaderAttribute) is HeaderAttribute csvAttr)
            {
                return csvAttr.Name;
            }
            else
            {
                return propertyInfo.Name;
            }
        }

        /// <summary>
        /// Creates an object per value separated line.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="values">Object values</param>
        /// <param name="header">Object header</param>
        /// <returns></returns>
        private T CreateObjectFromValues<T>(string[] values, string[] header) where T : new()
        {
            var newObject = new T();

            for (var i = 0; i < header.Length; i++)
            {
                var propertyInfo = newObject.GetType().GetProperty(header[i]);

                if (propertyInfo == null)
                {
                    continue;
                }

                if (propertyInfo.PropertyType == typeof(short)
                    || propertyInfo.PropertyType == typeof(short?))
                {
                    if (short.TryParse(values[i], out short res))
                    {
                        propertyInfo.SetValue(newObject, res, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(int)
                        || propertyInfo.PropertyType == typeof(int?))
                {
                    if (int.TryParse(values[i], out int res))
                    {
                        propertyInfo.SetValue(newObject, res, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(long)
                        || propertyInfo.PropertyType == typeof(long?))
                {
                    if (long.TryParse(values[i], out long res))
                    {
                        propertyInfo.SetValue(newObject, res, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(float)
                        || propertyInfo.PropertyType == typeof(float?))
                {
                    if (float.TryParse(values[i], out float res))
                    {
                        propertyInfo.SetValue(newObject, res, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(double)
                         || propertyInfo.PropertyType == typeof(double?))
                {
                    if (double.TryParse(values[i], out double res))
                    {
                        propertyInfo.SetValue(newObject, res, null);
                    }
                    else
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
                else
                {
                    try
                    {
                        var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                        if (Convert.ChangeType(values[i], type).ToString()?.Length == 0)
                        {
                            propertyInfo.SetValue(newObject, null, null);
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, Convert.ChangeType(values[i], type), null);
                        }
                    }
                    catch
                    {
                        propertyInfo.SetValue(newObject, null, null);
                    }
                }
            }

            return newObject;
        }

        /// <summary>
        /// Get value of property for source object
        /// </summary>
        /// <param name="source">Source object</param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>Returns value of the property.</returns>
        private string GetPropertyValue(object source, string propertyName)
        {
            var property = source.GetType().GetProperty(propertyName);
            var propertyValue = property.GetValue(source, null);

            if (Array.Find(property.GetCustomAttributes(true), x => x is FormatAttribute) is FormatAttribute formatAttribute)
            {
                return string.Format("{0:" + formatAttribute.Format + "}", propertyValue);
            }

            if (System.Array.Find(property.GetCustomAttributes(true), x => x is BoolToIntAttribute) is BoolToIntAttribute boolToIntAttribute
                && (property.PropertyType == typeof(Boolean)
                    || property.PropertyType == typeof(Boolean?)))
            {
                return ((bool)propertyValue) ? "1" : "0";
            }

            return propertyValue.ToString();
        }
    }
}
