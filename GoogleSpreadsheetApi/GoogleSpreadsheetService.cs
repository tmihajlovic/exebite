using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using GoogleSpreadsheetApi.Common.Attributes;
using GoogleSpreadsheetApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace GoogleSpreadsheetApi
{
    public class GoogleSpreadsheetService //: IGoogleSpreadsheetService
    {
        #region Fields
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        private UserCredential credential;
        private SheetsService service;
        private string _spreadSheet = "1Gke-Adx8QfevYGTn-D3a5RT-ARqQut_tChHWKwXMtuk";

        #endregion
        /// <summary>
        /// Google Spread Sheet API used for spread sheet manipulation
        /// </summary>
        /// <param name="secrets">Secret json file as bytes</param>
        /// <param name="credentialsPath">Path where to save credentials. Defualt value is Documents</param>
        public GoogleSpreadsheetService()
        {
            //Authorize();
            //InitService();
        }
        
        ///// <summary>
        ///// Creates Google Spreadsheet
        ///// </summary>
        ///// <param name="spreadsheetProperties">Spreadsheet properties</param>
        ///// <param name="list">List of IEnumerable objects which hold data values.</param>
        //public void Create(GoogleSpreadsheetProperties spreadsheetProperties, params IEnumerable<object>[] list)
        //{
        //    if (list == null || list.Length < 1)
        //    {
        //        return;
        //    }

        //    var spreadsheet = new Spreadsheet();
            
        //    var spreadsheetProp = GenerateSpreadsheetProperties(spreadsheetProperties);
            
        //    spreadsheet.Properties = spreadsheetProp;
            
        //    var sheets = new List<Sheet>();
        //    var updateRequest = new BatchUpdateValuesRequest();
        //    updateRequest.Data = new List<ValueRange>();
            
        //    for (int i = 0; i < list.Length; i++)
        //    {
        //        // Add spread sheets
        //        var sheet = new Sheet();
        //        sheet.Properties = new SheetProperties();

        //        // Get type of the item
        //        Type type = list[i].GetType().GetGenericArguments()[0];
        //        var attr = (SpreadsheetAttribute)type.GetCustomAttribute(typeof(SpreadsheetAttribute));
        //        if(attr != null && !string.IsNullOrWhiteSpace(attr.Name))
        //        {
        //            sheet.Properties.Title = attr.Name;
        //        }
        //        else
        //        {
        //            sheet.Properties.Title = type.Name;
        //        }

        //        sheets.Add(sheet);

        //        // Fill spread sheets
        //        FillSheet(ref updateRequest, sheet.Properties.Title, list[i]);
        //    }

        //    spreadsheet.Sheets = sheets;

        //    // Create spread sheet
        //    var createdSpreadsheetRequest = service.Spreadsheets.Create(spreadsheet);
        //    var createdSpreadsheetResponse = createdSpreadsheetRequest.Execute();

        //    // Fill data 
        //    var updateValueRequest = service.Spreadsheets.Values.BatchUpdate(updateRequest, createdSpreadsheetResponse.SpreadsheetId);
        //    var updateValueResponse = updateValueRequest.Execute();
        //}


        ///// <summary>
        ///// Gets all meal, from begning of time
        ///// </summary>
        ///// <returns>ValuRange of Meal,price and date</returns>
        //public IDictionary<string, FoodSpreadsheet> GetSSData()
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    var range = this.GetLastColumn();
        //    string SpreadSheetRange = sheet + "!C3:" + range + "7";
        //    SpreadsheetsResource.ValuesResource.GetRequest request =
        //                service.Spreadsheets.Values.Get(SpreadSheetID, SpreadSheetRange);
        //    request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
        //    ValueRange response = request.Execute();
            
        //    List<FoodSpreadsheet> mealList = new List<FoodSpreadsheet>();
        //    string tmpDate = "";
        //    foreach (var meal in response.Values)
        //    {
        //        if (meal.Count != 0)
        //        {

        //            if (meal[1].ToString() != "")
        //            {
        //                tmpDate = meal[1].ToString();
        //            }

        //            mealList.Add(new FoodSpreadsheet()
        //            {
        //                Name = meal[0].ToString(),
        //                Price = Convert.ToDouble(meal[3]),
        //                Date = tmpDate
        //            });
        //        }
        //    }

        //    mealList.Reverse();
        //    string rangeEnd = response.Range.Substring(response.Range.IndexOf(':') + 1);
        //    var lastColumn = new String(rangeEnd.TakeWhile(Char.IsLetter).ToArray());
        //    var currentColumn = lastColumn;

        //    Dictionary<string, FoodSpreadsheet> mealDtoList = new Dictionary<string, FoodSpreadsheet>();
        //    foreach (var meal in mealList)
        //    {
        //        mealDtoList[currentColumn] = meal;
        //        currentColumn = GetPrevious(currentColumn);
        //    }

        //    return mealDtoList;
        //}
        ///// <summary>
        ///// Get list od all users
        ///// </summary>
        ///// <returns>ValueRange of users</returns>
        //public IDictionary<int, string> GetUsers()
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    string SpreadSheetRange = sheet + "!B9:B1000";
        //    SpreadsheetsResource.ValuesResource.GetRequest request =
        //                service.Spreadsheets.Values.Get(SpreadSheetID, SpreadSheetRange);
        //    request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
        //    ValueRange response = request.Execute();

        //    Dictionary<int, string> users = new Dictionary<int, string>();
        //    int userRow = 9;// user names start on row 9
        //    foreach (var userData in response.Values[0])
        //    {
        //        users.Add(userRow, userData.ToString());
        //        userRow++;
        //    }

        //    return users;
        //}
        ///// <summary>
        ///// Check is meal is orderd
        ///// </summary>
        ///// <param name="userRow">Nummber of row in table </param>
        ///// <returns>True if meal is orderd for logged user, false otherwise</returns>
        //public bool IsOrderd(string userRow, string meal)
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    string SpreadSheetRange = sheet + "!" + meal + userRow;
        //    SpreadsheetsResource.ValuesResource.GetRequest request =
        //                service.Spreadsheets.Values.Get(SpreadSheetID, SpreadSheetRange);
        //    request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
        //    ValueRange response = request.Execute();
        //    if (response.Values != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        //public void PlaceOrder(string userRow, string meal)
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    string SpreadSheetRange = sheet + "!" + meal + userRow;
        //    ValueRange orderX = new ValueRange();
        //    var oblist = new List<object>() { "x" };
        //    orderX.Values = new List<IList<object>> { oblist };

        //    SpreadsheetsResource.ValuesResource.UpdateRequest request =
        //        service.Spreadsheets.Values.Update(orderX, SpreadSheetID, SpreadSheetRange);
        //    request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
        //    request.Execute();
        //}

        //public void CancelOrder(string userRow, string meal)
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    string SpreadSheetRange = sheet + "!" + meal + userRow;

        //    ClearValuesRequest body = new ClearValuesRequest();
        //    SpreadsheetsResource.ValuesResource.ClearRequest request =
        //        service.Spreadsheets.Values.Clear(body, SpreadSheetID, SpreadSheetRange);
        //    request.Execute();
        //}

        ///// <summary>
        ///// Gets curently active sheet
        ///// </summary>
        ///// <returns>String with sheet Title</returns>
        //private string GetActiveSheet()
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    SpreadsheetsResource.GetRequest request =
        //        service.Spreadsheets.Get(SpreadSheetID);
        //    request.Fields = "sheets(properties(hidden,index,sheetId,title))";
        //    var result = request.Execute();
        //    var sheet = result.Sheets.FirstOrDefault(s => s.Properties.Index != 0 && s.Properties.Hidden != true).Properties.Title;

        //    return sheet;
        //}

        //private string GetLastColumn()
        //{
        //    string SpreadSheetID = _spreadSheet;
        //    var sheet = this.GetActiveSheet();
        //    string SpreadSheetRange = sheet;
        //    SpreadsheetsResource.ValuesResource.GetRequest request =
        //                service.Spreadsheets.Values.Get(SpreadSheetID, SpreadSheetRange);
        //    request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
        //    request.Fields = "range";
        //    ValueRange response = request.Execute();
        //    string rangeEnd = response.Range.Substring(response.Range.IndexOf(':') + 1);
        //    var lastColumn = new String(rangeEnd.TakeWhile(Char.IsLetter).ToArray());

        //    return lastColumn;
        //}



        ///// <summary>
        ///// Fills spread sheet
        ///// </summary>
        ///// <param name="updateRequest">Update request which is used for all spread sheets</param>
        ///// <param name="sheetName">Name of the sheet</param>
        ///// <param name="enumerable">Data represented as IEnumerable object</param>
        //private void FillSheet(ref BatchUpdateValuesRequest updateRequest, string sheetName, IEnumerable<object> enumerable)
        //{
        //    // Get type of the item
        //    Type type = enumerable.GetType().GetGenericArguments()[0];

        //    // Extract properties of the item
        //    PropertyInfo[] properties = type.GetProperties();
            
        //    // Build A1 notation range for value update
        //    var startCell = BuildCell(1, 1);
        //    var endCell = BuildCell(enumerable.ToList().Count + 1, properties.Length);
        //    var rangePropString = BuildRange(sheetName, startCell, endCell);
        //    var vRange = new ValueRange() { Range = rangePropString };

        //    var allValues = new List<IList<object>>();

        //    // Add property names
        //    var propertyValues = new List<object>();
        //    properties.ToList().ForEach( prop =>
        //    {
        //        object[] attribute = prop.GetCustomAttributes(typeof(SpreadsheetAttribute), true);
        //        if (attribute.Length > 0)
        //        {
        //            SpreadsheetAttribute att = (SpreadsheetAttribute)attribute[0];
        //            propertyValues.Add(att.Name);
        //        }
        //        else
        //        {
        //            propertyValues.Add(prop.Name);
        //        }

        //    });
        //    allValues.Add(propertyValues);

        //    foreach (var item in enumerable)
        //    {
        //        // Add property values
        //        var valueList = new List<object>();
        //        properties.ToList().ForEach(prop => valueList.Add(prop.GetValue(item)));
        //        allValues.Add(valueList);
        //    }

        //    vRange.Values = allValues;

        //    updateRequest.ValueInputOption = GoogleInputOption.USER_ENTERED.ToString();
        //    updateRequest.Data.Add(vRange);
        //}




        ///// <summary>
        ///// Generates spreadsheet properties
        ///// </summary>
        ///// <param name="spreadsheetProperties">GoogleSpreadsheetProperties object containing values to be mapped</param>
        ///// <returns>Spreadsheet properties object</returns>
        //private SpreadsheetProperties GenerateSpreadsheetProperties(GoogleSpreadsheetProperties spreadsheetProperties)
        //{
        //    // Maps properties
        //    var sheetProperty = new SpreadsheetProperties()
        //    {
        //        Title = spreadsheetProperties.SpreadsheetTitle
        //    };

        //    return sheetProperty;
        //}

        
        ///// <summary>
        ///// Authorizes application and user
        ///// </summary>
        ///// <param name="secrets">Secret json file as bytes</param>
        ///// <param name="credentialsPath">Path where to save credentials. Defualt value is Documents</param>
        //private void Authorize()
        //{
        //    var location = @"C:\Users\nberdic\source\repos\FoodOrdering\GoogleSpreadsheetApi\Resources\client_secret.json"; 
        //    //TODO: Remove hardcoded path @nberdic @02/06/2018
        //    using (var stream = new FileStream(location, FileMode.Open))
        //    {
        //        string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        //        credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            Scopes,
        //            "user",
        //            CancellationToken.None).Result;
        //        Console.WriteLine("Credential file saved to: " + credPath);
        //    }
        //}

        ///// <summary>
        ///// Initializes Sheet service
        ///// </summary>
        //private void InitService()
        //{
        //    // Create Google Sheets API service.
        //    service = new SheetsService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });
        //}
        
        ///// <summary>
        ///// Calculates Excel column in alphabet
        ///// </summary>
        ///// <param name="columnNumber">Int number of column</param>
        ///// <returns>Alphabet string</returns>
        //private string GetExcelColumnName(int columnNumber)
        //{
        //    int dividend = columnNumber;
        //    string columnName = String.Empty;
        //    int modulo;

        //    while (dividend > 0)
        //    {
        //        modulo = (dividend - 1) % 26;
        //        columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
        //        dividend = (int)((dividend - modulo) / 26);
        //    }

        //    return columnName;
        //}

        ///// <summary>
        ///// Build range between two cells
        ///// </summary>
        ///// <param name="from">Cell from</param>
        ///// <param name="to">Cell to</param>
        ///// <returns>Google A1 range notation</returns>
        //private string BuildRange(string sheetName, string from, string to = null)
        //{
        //    if (to != null)
        //    {
        //        return $"{sheetName}!{from}:{to}";
        //    }
        //    else
        //    {
        //        return $"{sheetName}!{from}";
        //    }
        //}

        ///// <summary>
        ///// Builds cell
        ///// </summary>
        ///// <param name="row">Row number</param>
        ///// <param name="column">Column number</param>
        ///// <returns>A1 notation string</returns>
        //private string BuildCell(int row, int column)
        //{
        //    string excelColumnName = GetExcelColumnName(column);
        //    return excelColumnName + row.ToString();
        //}

        ///// <summary>
        ///// Input options fog google spread sheet
        ///// </summary>
        //enum GoogleInputOption
        //{
        //    RAW,
        //    USER_ENTERED
        //}

        //private string GetPrevious(string current)
        //{
        //    var last = current.Reverse().ToArray();
        //    if (last[0] != 'A')
        //    {
        //        last[0]--;
        //        return new String(last.Reverse().ToArray());
        //    }
        //    if (last[0] == 'A')
        //    {
        //        if (last.Count() == 1)
        //        {
        //            return "";
        //        }
        //        var next = new String(last.Skip(1).ToArray());
        //        var result = new String(("Z" + GetPrevious(next)).Reverse().ToArray());
        //        return result;
        //    }

        //    return "";
        //}
    }
}
