using System;
using System.Linq;

namespace Warehouse
{
    /// <summary>
    /// Entry point for the Warehouse exercise.
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            WarehouseServer server = new WarehouseServer();
            server.InitializeFromCsv("warehouse_pings.csv");
            Console.WriteLine("~~~Warehouse server is initialized");
            Console.WriteLine();

            var averageSpeeds = server.GetAverageSpeeds();
            var averageSpeedsText = string.Join(", ", averageSpeeds.Select(pair => $"{pair.Key}={pair.Value}"));
            Console.WriteLine($"Average speeds: {averageSpeedsText}");
            Console.WriteLine();

            PrintArray(
                "The 3 most traveled vehicles since 1553273158 are:",
                server.GetMostTraveledSince(3, 1553273158));

            PrintArray("Vehicles possibly damaged:", server.CheckForDamage());

            // Feel free to put any diagnostics statements below for testing and debugging.
        }

        private static void PrintArray(string description, string[] array)
        {
            Console.WriteLine(description);
            foreach (string element in array)
            {
                Console.WriteLine($"\t{element}");
            }
            Console.WriteLine();
        }
    }
}
