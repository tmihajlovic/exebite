﻿using ConsoleDebug.Logger;
using Exebite.Sheets.Hedone;
using Exebite.Sheets.Index;
using Exebite.Sheets.PodLipom;
using Exebite.Sheets.Teglas;
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

            var teg = new TeglasReader(logger);
            var DailyOffers = teg.ReadDailyOffers(providedDate);
            var PerpetualOffers = teg.ReadFoodItems();

            //var index = new IndexReader(logger);
            //var DailyOffers = index.ReadDailyOffers(providedDate);
            //var PerpetualOffers = index.ReadFoodItems();

            //var lipa = new PodLipomReader(logger);
            //var DailyOffers = lipa.ReadDailyOffers(providedDate);
            //var PerpetualOffers = lipa.ReadFoodItems();

            //var hed = new HedoneReader(logger);
            //var DailyOffers = hed.ReadDailyOffers(providedDate);
            //var PerpetualOffers = hed.ReadFoodItems();


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