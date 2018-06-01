using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Test.Mocks
{
    public class GoogleSheetServiceFake : IGoogleSheetService
    {
        private static string dailyMenuSheet = "Dnevni meni";
        private static string alwaysAvailableSheet = "Uvek dostupno";
        private static string menuSheet = "Meni";
        private static string foodListSheet = "Cene i opis";
        private static string kasaSheet = "Kasa";
        private static string ordersSheet = "Narudzbine";

        public void Clear(string sheetId, string range)
        {
            return;
        }

        public ValueRange GetColumns(string sheetId, string range)
        {
            ValueRange result = new ValueRange();

            if (range == dailyMenuSheet)
            {
                result = FakeSheetDataFactory.GetAllSheetDaily();
            }
            else if (range.Contains(dailyMenuSheet))
            {
                result = FakeSheetDataFactory.GetDailyMenu();
            }
            else if (range.Contains(alwaysAvailableSheet))
            {
                result = FakeSheetDataFactory.GetAlwaysAvailableMenu();
            }
            else if (range.Contains(menuSheet))
            {
                result = FakeSheetDataFactory.GetAlwaysAvailableMenu();
            }

            result.MajorDimension = "COLUMNS";
            result.Range = range;
            return result;
        }

        public ValueRange GetRows(string sheetId, string range)
        {
            ValueRange result = new ValueRange();
            result = FakeSheetDataFactory.GetAllFoods();
            result.MajorDimension = "ROWS";
            result.Range = range;
            return result;
        }

        public void Update(ValueRange body, string sheetId, string range)
        {
            if (range == dailyMenuSheet)
            {
                var dateToday = body.Values[1][0].ToString();
                if (dateToday != DateTime.Today.Date.ToString("dd-MM-yyyy"))
                {
                    throw new Exception("Today date wrong!");
                }

                var dateTomorow = body.Values[1][1].ToString();
                int addDays = CheckDate(dateToday);

                if (dateTomorow != DateTime.Today.AddDays(addDays).Date.ToString("dd-MM-yyyy"))
                {
                    throw new Exception("Tomorow date wrong!");
                }
            }
            else if (range == foodListSheet)
            {
                if (body.Values[0][0].ToString() != "Naziv jela")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[0][1].ToString() != "Opis")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[1][0].ToString() != "Food 1")
                {
                    throw new Exception("Bad data formating");
                }
            }
            else if (range == kasaSheet)
            {
                if (body.Values[0][1].ToString() != "Ime i prezime")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[0][2].ToString() != "Suma")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[1][1].ToString() != "Test Customer 1")
                {
                    throw new Exception("Bad data formating");
                }
            }
            else if (range == ordersSheet)
            {
                if (body.Values[0][0].ToString() != "Jelo")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[0][1].ToString() != "Komada")
                {
                    throw new Exception("Bad data formating");
                }

                if (body.Values[1][0].ToString() != "Food 1")
                {
                    throw new Exception("Bad data formating");
                }
            }
        }

        private int CheckDate(string dateToday)
        {
            var checkDate = DateTime.Parse(dateToday);
            int addDays = 1;
            if (checkDate.DayOfWeek == DayOfWeek.Friday)
            {
                addDays = 3;
            }

            return addDays;
        }
    }
}
