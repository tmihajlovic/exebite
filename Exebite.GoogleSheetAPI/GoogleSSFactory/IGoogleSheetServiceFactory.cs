using Google.Apis.Sheets.v4;

namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public interface IGoogleSheetServiceFactory
    {
        /// <summary>
        /// Create Google sheet factory
        /// </summary>
        /// <returns>New <see cref="SheetsService"/></returns>
        SheetsService GetService();
    }
}
