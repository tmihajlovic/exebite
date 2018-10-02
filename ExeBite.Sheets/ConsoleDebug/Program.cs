using ConsoleDebug.Logger;
using ExeBite.Sheets.Hedone;
using ExeBite.Sheets.Index;
using ExeBite.Sheets.PodLipom;
using ExeBite.Sheets.Teglas;
using System;
using System.Globalization;
using System.Linq;

namespace ConsoleDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test Started");
            var logger = new ConsoleLogger();

            // This is where you write tests with the Sheet readers and writers.
            var providedDate = DateTime.ParseExact("9/3/2018", "M/d/yyyy", CultureInfo.InvariantCulture);

            //var hed = new HedoneReader(logger);
            //var DailyOffers = hed.ReadDailyOffers(providedDate);
            //var PerpetualOffers = hed.ReadFoodItems();

            //var lipa = new PodLipomReader(logger);
            //var DailyOffers = lipa.ReadDailyOffers(providedDate);
            //var PerpetualOffers = lipa.ReadFoodItems();

            //var teg = new TeglasReader(logger);
            //var DailyOffers = teg.ReadDailyOffers(providedDate);
            //var PerpetualOffers = teg.ReadFoodItems();

            var index = new IndexReader(logger);
            var DailyOffers = index.ReadDailyOffers(providedDate);
            var PerpetualOffers = index.ReadFoodItems();

            Console.WriteLine("Daily offers:");
            DailyOffers.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("------------------------");
            Console.WriteLine("Standard offers:");
            PerpetualOffers.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Test ended. Press any key to close...");
            Console.ReadKey();
        }
    }
}
