using System;

namespace Exebite.Converters.Attributes
{
    /// <summary>
    /// Represent boolean that needs to be converted to int
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BoolToIntAttribute : Attribute
    {
    }
}
