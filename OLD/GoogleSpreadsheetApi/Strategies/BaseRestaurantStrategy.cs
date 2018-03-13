using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exebite.GoogleSpreadsheetApi.Strategies
{
    public class BaseRestaurantStrategy
    {
        private static readonly List<string> DateTimeFormats = new List<string>
        {
            "dd.MM.yyyy.",
            "dd.M.yyyy.",
            "d.MM.yyyy.",
            "d.M.yyyy.",
            "dd.MM.yyyy",
            "dd.M.yyyy",
            "d.MM.yyyy",
            "d.M.yyyy",
            " dd.MM.yyyy.",
            " dd.M.yyyy.",
            " d.MM.yyyy.",
            " d.M.yyyy."
        };

        protected SheetsService service;

        public BaseRestaurantStrategy(IGoogleSheetServiceFactory GoogleSSFactory)
        {
            //var GoogleSS = new GoogleSheetServiceFactory();
            service = GoogleSSFactory.GetService();
        }

        protected string[][] MapFields(string sheetId, string sheetName)
        {
            var command = service.Spreadsheets.Values.Get(sheetId, $"{ sheetName }");

            var response = command.Execute();

            int columnNumber = Convert.ToInt32(response.Values.Max(r => r.Count));
            string[][] mappedValues = new string[response.Values.Count][];

            for (int i = 0; i < response.Values.Count; i++)
            {
                mappedValues[i] = new string[columnNumber];
                for (int j = 0; j < response.Values[i].Count; j++)
                {
                    if(response.Values[i].Count >= j)
                        mappedValues[i][j] = response.Values[i][j].ToString();
                }
            }

            return mappedValues;
        }


        protected IList<IList<object>> MapFields2(string sheetId, string sheetName)
        {
            var command = service.Spreadsheets.Values.Get(sheetId, $"{ sheetName }");

            var response = command.Execute();

            return response.Values;
            //int columnNumber = Convert.ToInt32(response.Values.Max(r => r.Count));
            //string[][] mappedValues = new string[response.Values.Count][];

            //for (int i = 0; i < response.Values.Count; i++)
            //{
            //    mappedValues[i] = new string[columnNumber];
            //    for (int j = 0; j < response.Values[i].Count; j++)
            //    {
            //        if (response.Values[i].Count >= j)
            //            mappedValues[i][j] = response.Values[i][j].ToString();
            //    }
            //}

            //return mappedValues;
        }

        /// <summary>
        /// Calculates Excel column in alphabet
        /// </summary>
        /// <param name="columnNumber">Int number of column</param>
        /// <returns>Alphabet string</returns>
        private string GetExcelColumnName(int columnNumber)
        {
            if(columnNumber < 0)
            {
                throw new ArgumentException();
            }

            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        /// <summary>
        /// Gets column number according to column name
        /// </summary>
        /// <param name="columnName">Name of the column in A1 notaiton</param>
        /// <returns>Column number</returns>
        protected int GetExcelColumnNumber(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentNullException();
            }

            columnName = columnName.ToUpperInvariant();

            int sum = 0;

            for (int i = 0; i < columnName.Length; i++)
            {
                sum *= 26;
                sum += (columnName[i] - 'A' + 1);
            }

            return sum;
        }


        protected string[][] GetDateData(string[][] fullData, int startColumn, int columnOffset)
        {
            string[][] newArray = new string[fullData.Length][];

            for (int i = 0; i < fullData.Length; i++)
            {
                newArray[i] = new string[columnOffset - 1];

                for (int j = 0; j < columnOffset - 1; j++)
                {
                    newArray[i][j] = fullData[i][startColumn + j];
                }
            }

            return newArray;
        }

        protected DateTime ParseDate(string time)
        {
            DateTime date;

            foreach (var format in DateTimeFormats)
            {
                try
                {
                    date = DateTime.ParseExact(time, format, System.Globalization.CultureInfo.InvariantCulture);
                    return date;
                }
                catch (Exception)
                {
                }
            }

            throw new FormatException();
        }

        /// <summary>
        /// Build range between two cells
        /// </summary>
        /// <param name="from">Cell from</param>
        /// <param name="to">Cell to</param>
        /// <returns>Google A1 range notation</returns>
        private string BuildRange(string sheetName, string from, string to = null)
        {
            if (to != null)
            {
                return $"{sheetName}!{from}:{to}";
            }
            else
            {
                return $"{sheetName}!{from}";
            }
        }

        /// <summary>
        /// Builds cell
        /// </summary>
        /// <param name="row">Row number</param>
        /// <param name="column">Column number</param>
        /// <returns>A1 notation string</returns>
        private string BuildCell(int row, int column)
        {
            string excelColumnName = GetExcelColumnName(column);
            return excelColumnName + row.ToString();
        }
        
    }
}
