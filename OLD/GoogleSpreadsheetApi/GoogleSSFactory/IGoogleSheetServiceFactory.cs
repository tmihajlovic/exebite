using Google.Apis.Sheets.v4;

namespace Exebite.GoogleSpreadsheetApi.GoogleSSFactory
{
    public interface IGoogleSheetServiceFactory
    {
        SheetsService GetService();
    }
}
