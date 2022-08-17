using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse
{
    /// <summary>
    /// A named vehicle with a sequence of pings.
    /// </summary>
    public sealed class Vehicle
    {
        /// <summary>
        /// The name of the vehicle.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The pings for the vehicle, in chronological order (earliest first).
        /// </summary>
        public List<Ping> Pings { get; } = new List<Ping>();

        public Vehicle(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Determines the total distance covered by the pings.
        /// </summary>
        /// <returns>The total distance.</returns>
        private static double GetTotalDistance(IEnumerable<Ping> pings)
        {
            return pings
                .Zip(pings.Skip(1), CalculateDistance)
                .Sum();
        }

        private static double CalculateDistance(Ping currentPing, Ping nextPing)
        {
            var x1 = currentPing.Position.X;
            var y1 = currentPing.Position.Y;
            var x2 = nextPing.Position.X;
            var y2 = nextPing.Position.Y;
            var distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            
            return distance;
        }

        /// <summary>
        /// Determines the total distance traveled by the vehicle.
        /// </summary>
        /// <returns>The total distance.</returns>
        public double GetTotalDistance()
        {
            return GetTotalDistance(Pings);
        }

        /// <summary>
        /// Determines the total distance traveled by the vehicle before the timestamp.
        /// </summary>
        /// <returns>The total distance.</returns>
        public double GetTotalDistanceSince(long timestamp)
        {
            var availablePings = Pings
                .Where(p => p.Timestamp > timestamp);
            
            return GetTotalDistance(availablePings);
        }

        /// <summary>
        /// Determines the average speed of the vehicle.
        /// </summary>
        /// <returns>The average speed of the vehicle.</returns>
        public double GetAverageSpeed()
        {
            var firstPing = Pings.First().Timestamp;
            var lastPing = Pings.Last().Timestamp;
            var totalTime = lastPing - firstPing;
            
            return GetTotalDistance() / totalTime;
        }
    }
}
