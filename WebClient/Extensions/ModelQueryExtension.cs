using System.Web;

namespace WebClient.Extensions
{
    public static class ModelQueryExtension
    {
        public static string BuildQuery<T>(this T queryObject)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);

            var props = queryObject.GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(queryObject);
                query[prop.Name] = value != null ? value.ToString() : string.Empty;
            }

            return query.ToString();
        }
    }
}