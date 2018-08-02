using System.Collections.Generic;

namespace Exebite.Converters
{
    /// <summary>
    /// Value separated converter
    /// </summary>
    public interface IValueSepartedConverter
    {
        /// <summary>
        /// Accepts a list of objects to be serialized to value separated file.
        /// </summary>
        /// <typeparam name="T">Object definition to be serialized</typeparam>
        /// <param name="input">Collection of objects to be serialized</param>
        /// <param name="delimiter"> Delimiter to be used</param>
        /// <returns>CSV string</returns>string Serialize<T>(IEnumerable<T> input, string delimiter = ";") where T : class;
        string Serialize<T>(IEnumerable<T> input, string delimiter = ";") where T : class;

        /// <summary>
        /// Deserializes the value separated file and maps output to collection of objects.
        /// </summary>
        /// <typeparam name="T">Type of the parameter</typeparam>
        /// <param name="inputLines">Lines to be split</param>
        /// <param name="delimiter">Delimiter of the values</param>
        /// <returns></returns>
        IEnumerable<T> Deserialize<T>(string[] inputLines, string delimiter = ",") where T : new();
    }
}
