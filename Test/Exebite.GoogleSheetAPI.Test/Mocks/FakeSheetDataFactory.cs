using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;

namespace Exebite.GoogleSheetAPI.Test.Mocks
{
    public static class FakeSheetDataFactory
    {
        public static ValueRange GetDailyMenu()
        {
            ValueRange result = new ValueRange
            {
                Values = new List<IList<object>>
            {
                new List<object> { "Test food 1", "Test food 2", "Test food 3" },
                new List<object> { "Test food 1", "Test food 8" },
                new List<object> { "Test food 4", "Test food 2", "Test food 6" },
                new List<object> { "Test food 4", "Test food 2", "Test food 6", "Test food 3" }
            }
            };
            return result;
        }

        public static ValueRange GetAlwaysAvailableMenu()
        {
            ValueRange result = new ValueRange
            {
                Values = new List<IList<object>>
            {
                new List<object> { "Test aa food 1", "Test aa food 2", "Test aa food 3" }
            }
            };
            return result;
        }

        public static ValueRange GetAllFoods()
        {
            ValueRange result = new ValueRange
            {
                Values = new List<IList<object>>()
            };
            result.Values.Add(new List<object> { "Naziv jela", "Opis", "Cena", "Tip" });
            result.Values.Add(new List<object> { "Test food 1", "Description 1", "100", "Glavno jelo" });
            result.Values.Add(new List<object> { "Test food 2", "Description 2", "200", "Glavno jelo" });
            result.Values.Add(new List<object> { "Test food 3", "Description 3", "300", "Glavno jelo" });
            result.Values.Add(new List<object> { "Test food 4", "Description 4", "400", "Desert" });

            return result;
        }

        public static ValueRange GetAllSheetDaily()
        {
            ValueRange result = new ValueRange
            {
                Values = new List<IList<object>>
            {
                new List<object> { "Ponedeljak", "26-2-2018", "Test food 1", "Test food 2", "Test food 3" },
                new List<object> { "Utorak", "27-2-2018", "Test food 1", "Test food 8" },
                new List<object> { "Sreda", "28-2-2018", "Test food 4", "Test food 2", "Test food 6" },
                new List<object> { "Cetvrtak", "1-3-2018", "Test food 4", "Test food 2", "Test food 6", "Test food 3" },
                new List<object> { "Petak", "2-3-2018", "Test food 4", "Test food 2", "Test food 6", "Test food 3" }
            }
            };
            return result;
        }
    }
}
