using System;
using System.Collections.Generic;
using System.Globalization;
using Exebite.GoogleSheetAPI.Common;
using Exebite.GoogleSheetAPI.GoogleSSFactory;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.SheetExtractor
{
    public sealed class GoogleSheetExtractor : IGoogleSheetExtractor
    {
        private readonly SheetsService _sheetService;
        private readonly IGoogleSheetServiceFactory _googleSSFactory;

        public GoogleSheetExtractor(IGoogleSheetServiceFactory googleSheetServiceFactory)
        {
            _googleSSFactory = googleSheetServiceFactory;
            _sheetService = _googleSSFactory.GetService();
        }

        public ValueRange GetRows(string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
            _sheetService.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.ROWS;
            ValueRange result = request.Execute();

            return result;
        }

        public ValueRange GetColumns(string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request =
            _sheetService.Spreadsheets.Values.Get(sheetId, range);
            request.MajorDimension = SpreadsheetsResource.ValuesResource.GetRequest.MajorDimensionEnum.COLUMNS;
            ValueRange result = request.Execute();

            return result;
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = _sheetService.Spreadsheets.Values.Update(body, sheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            updateRequest.Execute();
        }

        public void Clear(string sheetId, string range)
        {
            ClearValuesRequest body = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clearRequest = _sheetService.Spreadsheets.Values.Clear(body, sheetId, range);
            clearRequest.Execute();
        }

        /// <summary>
        /// Returns a list of worksheets in current sheet document.
        /// </summary>
        /// <param name="sheetId">Sheet ID that needs to be returned.</param>
        /// <returns>Google sheet with specified ID.</returns>
        public IEnumerable<Sheet> GetWorkSheets(string sheetId)
        {
            try
            {
                return _sheetService
                        .Spreadsheets
                        .Get(sheetId)
                        .Execute()
                        .Sheets;
            }
            catch
            {
                return new List<Sheet>();
            }
        }

        /// <summary>
        /// Reads DateTime in OLE Automation format and returns it as DateTime
        /// Will only read first cell in the range.
        /// </summary>
        /// <param name="range">Range from which to be read.</param>
        /// <param name="sheetId">Sheet ID from which to read range.</param>
        /// <returns>DateTime if possible to read value.</returns>
        public Result<DateTime> ReadDateTime(string range, string sheetId)
        {
            var readResult = ReadOneValue(range, sheetId);

            if (readResult.IsSuccess)
            {
                if (long.TryParse(readResult.Value.ToString(), out long readValue))
                {
                    try
                    {
                        var resultingDateTime = DateTime.FromOADate(readValue);
                        return Result<DateTime>.Success(resultingDateTime);
                    }
                    catch (Exception ex)
                    {
                        return Result<DateTime>.Fail(DateTime.MinValue, ex.Message);
                    }
                }

                return Result<DateTime>.Fail(DateTime.MinValue, $"Could not parse {readResult.Value} into long");
            }

            return Result<DateTime>.Fail(DateTime.MinValue, readResult.ErrorMessage);
        }

        /// <summary>
        /// Reads Data from the specified range in the sheet
        /// Returns empty ValueRange if some error has happen.
        /// </summary>
        /// <param name="range">Range to be read</param>
        /// <param name="sheetId">Sheet ID from which to read range.</param>
        /// <returns> Value range for specified range.</returns>
        public ValueRange ReadSheetData(string range, string sheetId)
        {
            try
            {
                var request = _sheetService
                                .Spreadsheets
                                .Values
                                .Get(sheetId, range);

                // We need to set this in order to get date-time properly.
                request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE;

                return request.Execute();
            }
            catch
            {
                return new ValueRange();
            }
        }

        public T ExtractCell<T>(IList<object> objectList, int index, T defaultValue)
        {
            T retVal;

            try
            {
                retVal = (T)Convert.ChangeType(objectList[index], typeof(T), CultureInfo.InvariantCulture);

                if (retVal is string a && string.IsNullOrWhiteSpace(a))
                {
                    retVal = defaultValue;
                }
            }
            catch (Exception)
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        /// <summary>
        /// Will only return first value in the provided range
        /// </summary>
        /// <param name="range">Which needs to be read</param>
        /// <param name="sheetId">Sheet ID from which to read range.</param>
        /// <returns>Return value from specified range.</returns>
        private Result<object> ReadOneValue(string range, string sheetId)
        {
            var readSheetResult = ReadSheetData(range, sheetId);

            if (readSheetResult.Values != null
                && readSheetResult.Values[0] != null
                && readSheetResult.Values[0][0] != null)
            {
                return Result<object>.Success(readSheetResult.Values[0][0]);
            }

            return Result<object>.Fail(new object(), "Could not find value in that range");
        }
    }
}
