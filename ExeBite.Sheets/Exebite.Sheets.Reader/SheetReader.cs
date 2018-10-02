using Exebite.Sheets.Common;
using Exebite.Sheets.Common.Interfaces;
using Exebite.Sheets.Common.Util;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;

namespace Exebite.Sheets.Reader
{
    public class SheetReader
    {
        #region private members
        private ILogger _logger;
        private readonly string _currentSheet;
        private Credentials _credentials;
        private ServiceProvider _service;

        private readonly string[] READ_SCOPES = { SheetsService.Scope.SpreadsheetsReadonly };
        #endregion

        #region Constructors and helper classes
        /// <summary>
        /// Creates sheet reader with sheet ID and passed logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="sheetId"></param>
        public SheetReader(ILogger logger, string sheetId)
        {
            _logger = logger;
            _currentSheet = sheetId;

            // This can fail.
            InitializeComponents();
        }

        /// <summary>
        /// Initializes all the components.
        /// Used to supplement Constructor.
        /// </summary>
        private void InitializeComponents()
        {
            try
            {
                _credentials = new Credentials(_logger, READ_SCOPES);
                _service = new ServiceProvider(_logger, _credentials.UserCredential);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Authorization failed with message: {ex.Message}");
                throw;
            }
        }
        #endregion
       
        /// <summary>
        /// Reads Data from tje specified range in the sheet
        /// Returns empty ValueRange if some error has happend.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public ValueRange ReadSheetData(string range)
        {
            try
            {
                var request = _service.GetInstance()
                        .Spreadsheets
                        .Values
                        .Get(_currentSheet, range);

                // We need to set this in order to get datetime properly.
                request.ValueRenderOption = SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE;

                return request.Execute();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could Not read sheet data in range {range}, error message: {ex.Message}");
                return new ValueRange();
            }
        }

        /// <summary>
        /// Will only return first value in the provided range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private Result<object> ReadOneValue(string range)
        {
            var readSheetResult = ReadSheetData(range);

            if (readSheetResult.Values != null
                && readSheetResult.Values[0] != null
                && readSheetResult.Values[0][0] != null)
            {
                return Result<object>.Success(readSheetResult.Values[0][0]);
            }

            return Result<object>.Fail(new object(), "Could not find value in that range");
        }

        /// <summary>
        /// Reads DateTime in OLE Automation format and returns it as DateTime
        /// Will only read first cell in the range.
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public Result<DateTime> ReadDateTime(string range)
        {
            var readResult = ReadOneValue(range);

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
        /// Returns a list of worksheets in current sheet document.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Sheet> GetWorkSheets()
        {
            try
            {
                return _service.GetInstance()
                        .Spreadsheets
                        .Get(_currentSheet)
                        .Execute()
                        .Sheets;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could Not read worksheets, error message: {ex.Message}");
                return new List<Sheet>();
            }
        }

    }
}
