using System;

namespace Exebite.Converters.Attributes
{
    /// <summary>
    /// This attribute is used for formating DateTime in CSV converter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FormatAttribute : Attribute
    {
        /// <summary>
        /// Output format
        /// </summary>
        public string Format { get; set; }
    }
}
