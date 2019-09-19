using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.SheetExtractor
{
    public interface IGoogleSheetExtractor
    {
        /// <summary>
        /// Gets data from sheet grouped by rows
        /// </summary>
        /// <param name="sheetId">Id of spreadsheet</param>
        /// <param name="range">Range to get</param>
        /// <returns><see cref="ValueRange"/> with data</returns>
        ValueRange GetRows(string sheetId, string range);

        /// <summary>
        /// Gets data from sheet grouped by columns
        /// </summary>
        /// <param name="sheetId">Id of spreadsheet</param>
        /// <param name="range">Range to get</param>
        /// <returns><see cref="ValueRange"/> with data</returns>
        ValueRange GetColumns(string sheetId, string range);

        /// <summary>
        /// Write data to sheet
        /// </summary>
        /// <param name="body"><see cref="ValueRange"/> with data to write, grouped by row</param>
        /// <param name="sheetId">Id of spreadsheet</param>
        /// <param name="range">Range to write data to</param>
        void Update(ValueRange body, string sheetId, string range);

        /// <summary>
        /// Clear data from given range in spreadsheet
        /// </summary>
        /// <param name="sheetId">Id of spreadsheet</param>
        /// <param name="range">Range to be cleard</param>
        void Clear(string sheetId, string range);

        /// <summary>
        /// Try to extract and convert value from the sheet onto the object. If it fails, it will return the default value.
        /// This method makes sure there's always a value, even if an exception occurs.
        /// </summary>
        /// <typeparam name="T">Data Type of the cell.</typeparam>
        /// <param name="objectList">List of objects.</param>
        /// <param name="index">Index to access.</param>
        /// <param name="defaultValue">Default value to return in case of an error.</param>
        /// <returns>T</returns>
        T ExtractCell<T>(IList<object> objectList, int index, T defaultValue);
    }
}