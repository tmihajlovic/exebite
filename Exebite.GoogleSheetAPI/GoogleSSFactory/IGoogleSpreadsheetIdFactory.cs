namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public interface IGoogleSpreadsheetIdFactory
    {
        /// <summary>
        /// Returns Hedone Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetHedone();

        /// <summary>
        /// Returns Lipa Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetLipa();

        /// <summary>
        /// Returns Index house Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetIndexHouse();

        /// <summary>
        /// Returns Extra food Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetExtraFood();

        /// <summary>
        /// Returns Teglas Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetTeglas();

        /// <summary>
        /// Returns Kasa Spredsheet ID
        /// </summary>
        /// <returns></returns>
        string GetKasa();
    }
}
