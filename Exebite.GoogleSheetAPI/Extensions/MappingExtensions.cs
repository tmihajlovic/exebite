namespace Exebite.GoogleSheetAPI.Extensions
{
    public static class MappingExtensions
    {
        /// <summary>
        /// Check whether strings have the same (trimmed / lower invariant cased) value.
        /// </summary>
        /// <param name="first">Base string.</param>
        /// <param name="compareTo">String to compare it to.</param>
        /// <returns>True/False depending on whether strings are of equal value.</returns>
        public static bool TrimmedAndLowercasedEqualsTo(this string first, string compareTo)
        {
            return first.Trim().ToLowerInvariant().Equals(compareTo.Trim().ToLowerInvariant());
        }
    }
}