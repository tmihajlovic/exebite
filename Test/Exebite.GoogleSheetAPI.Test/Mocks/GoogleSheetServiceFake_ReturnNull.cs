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
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        public ValueRange GetRows(string sheetId, string range)
        {
#pragma warning disable RETURN0001 // Do not return null
            return null;
#pragma warning restore RETURN0001 // Do not return null
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
        }
    }
}
