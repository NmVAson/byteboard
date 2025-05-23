﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Warehouse
{
    public sealed class WarehouseServer
    {
        private const double AggressiveAcceleration = 10.0;
        public List<Vehicle> Vehicles { get; } = new List<Vehicle>();

        /// <summary>
        /// Adds the information in the given CSV file to the warehouse data.
        /// </summary>
        /// <param name="path">The path to the CSV file.</param>
        public void InitializeFromCsv(string path)
        {
            try
            {
                var parsedLines = File.ReadLines(path)
                    .Select(line =>
                    {
                        string[] parts = line.Split(",");
                        string name = parts[0];
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);
                        long timestamp = long.Parse(parts[3]);
                        Ping ping = new Ping(x, y, timestamp);
                        return new { name, ping };
                    });
                foreach (var parsedLine in parsedLines)
                {
                    AddPing(parsedLine.name, parsedLine.ping);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"Exception thrown populating data: {e}");
            }
        }

        private void AddPing(string name, Ping ping)
        {
            Vehicle vehicle = Vehicles.FirstOrDefault(v => v.Name == name);
            if (vehicle == null)
            {
                vehicle = new Vehicle(name);
                Vehicles.Add(vehicle);
            }
            vehicle.Pings.Add(ping);
        }

        /// <summary>
        /// Returns a dictionary mapping each vehicle name to that vehicle's average speed, for all vehicles.
        /// </summary>
        internal Dictionary<string, double> GetAverageSpeeds() =>
            Vehicles.ToDictionary(v => v.Name, v => v.GetAverageSpeed());

        /// <summary>
        /// Returns a sorted array of size <paramref name="maxResults"/> of vehicle names corresponding to the vehicles
        /// that have traveled the most distance since the given timestamp.
        /// </summary>
        /// <param name="maxResults">The maximum number of results to return.</param>
        /// <param name="timestamp">The inclusive lower bound to consider.</param>
        /// <returns>A sorted array of vehicle names.</returns>
        internal string[] GetMostTraveledSince(int maxResults, long timestamp)
        {
            return Vehicles
                .OrderByDescending(v => v.GetTotalDistanceSince(timestamp))
                .ThenBy(v => v.Name)
                .Take(maxResults)
                .Select(v => v.Name)
                .ToArray();
        }

        /// <summary>
        /// Returns an array of names identifying vehicles that might have been damaged
        /// through any number of risky behaviors, including collision with another vehicle
        /// and excessive acceleration.
        /// </summary>
        internal string[] CheckForDamage()
        {
            var vehiclesWithAbnormalAcceleration = Vehicles
                .Where(v => v.GetMaxAcceleration() >= AggressiveAcceleration);

            var vehiclesColliding = Vehicles
                .SelectMany(v => v.Pings)
                .GroupBy(p => p)
                .Where(g => g.Count() > 1)
                .SelectMany(g => Vehicles.Where(v => v.Pings.Contains(g.Key)));

            var vehiclesDrivingRecklessly = vehiclesWithAbnormalAcceleration
                .Union(vehiclesColliding)
                .Select(v => v.Name);
                
            return vehiclesDrivingRecklessly.ToArray();
        }
    }
}
