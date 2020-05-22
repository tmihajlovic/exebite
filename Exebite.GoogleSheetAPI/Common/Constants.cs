namespace Exebite.GoogleSheetAPI.Common
{
    public sealed class Constants
    {
        /// <summary>
        /// App name used to identify this app to google servers.
        /// </summary>
        public const string APP_NAME = "Exebite Sheet Overseer";

        /// <summary>
        /// Name of Topli Obrok restaurant
        /// </summary>
        public const string TOPLI_OPBROK_NAME = "Topli Obrok";

        /// <summary>
        /// Name of the Index House restaurant
        /// </summary>
        public const string INDEX_NAME = "Index House";

        /// <summary>
        /// Name of the Pod Lipom Restaurant
        /// </summary>
        public const string POD_LIPOM_NAME = "Pod Lipom";

        /// <summary>
        /// Name of the Hey Day Restaurant
        /// </summary>
        public const string HEY_DAY_NAME = "Hey Day";

        /// <summary>
        /// Name of the Parrilla restaurant
        /// </summary>
        public const string PARRILLA_NAME = "Parilla";

        /// <summary>
        /// Name of the Mimas restaurant
        /// </summary>
        public const string MIMAS_NAME = "Mimas";

        /// <summary>
        /// Name of the Serpica restaurant
        /// </summary>
        public const string SERPICA_NAME = "Serpica";

        /// <summary>
        /// Category is not specified.
        /// </summary>
        public const string CATEGORY_NONE = "Unspecified";

        /// <summary>
        /// Offer is standard everyday offer
        /// </summary>
        public const string CATEGORY_STANDARD = "Standard Offer";

        /// <summary>
        /// Offer is in the daily offer category.
        /// </summary>
        public const string CATEGORY_DAILY = "Daily Offer";

        /// <summary>
        /// Sleep time between two requests, in miliseconds.
        /// </summary>
        public const int SLEEP_TIME = 50;
    }
}