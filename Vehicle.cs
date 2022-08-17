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
            var queuedPings = new Queue<Ping>(pings);
            var totalDistance = 0.0;
            while (queuedPings.TryDequeue(out var ping) && queuedPings.TryPeek(out var nextPing))
            {
                var x1 = ping.Position.X;
                var y1 = ping.Position.Y;
                var x2 = nextPing.Position.X;
                var y2 = nextPing.Position.Y;
                var distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));

                totalDistance += distance;
            }
            
            return totalDistance;
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
