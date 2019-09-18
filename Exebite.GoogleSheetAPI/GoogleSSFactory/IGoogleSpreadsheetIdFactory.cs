using Exebite.GoogleSheetAPI.Enums;

namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public interface IGoogleSpreadsheetIdFactory
    {
        /// <summary>
        /// Get Sheet ID for the specified Sheet owner.
        /// </summary>
        /// <param name="sheetOwner">ESheetOwner</param>
        /// <returns>String value of Google Sheet ID</returns>
        string GetSheetId(ESheetOwner sheetOwner);
    }
}
