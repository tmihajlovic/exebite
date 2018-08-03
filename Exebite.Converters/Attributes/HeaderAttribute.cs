using System;

namespace Exebite.Converters.Attributes
{
    /// <summary>
    /// This attribute is used for setting property name as CSV header.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class HeaderAttribute : Attribute
    {
        /// <summary>
        /// Property name as CSV header.
        /// </summary>
        public string Name { get; set; }
    }
}
