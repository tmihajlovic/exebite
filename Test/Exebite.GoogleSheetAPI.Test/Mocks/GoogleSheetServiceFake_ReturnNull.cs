using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Test.Mocks
{
    public class GoogleSheetServiceFake_ReturnNull : IGoogleSheetService
    {
        public void Clear(string sheetId, string range)
        {
        }

        public ValueRange GetColumns(string sheetId, string range)
        {
            return null;
        }

        public ValueRange GetRows(string sheetId, string range)
        {
            return null;
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
        }
    }
}
