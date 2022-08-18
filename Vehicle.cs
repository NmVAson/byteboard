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
                .Zip(pings.Skip(1), Ping.CalculateDistance)
                .Sum();
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
            var totalTime = Ping.SecondsBetween(Pings.First(), Pings.Last());
            
            return GetTotalDistance() / totalTime;
        }

        public double GetMaxAcceleration()
        {
            var acceleration = Pings.Zip(Pings.Skip(1), CalculateAcceleration).ToList();
            
            return acceleration.Any() 
                ? acceleration.Max()
                : 0.0;
        }

        private static double CalculateAcceleration(Ping ping, Ping nextPing)
        {
            const double initialVelocity = 0.0;
            var distanceFromLastPing = Ping.CalculateDistance(ping, nextPing);
            var timespanFromLastPing = Ping.SecondsBetween(ping, nextPing);

            return 2 * (distanceFromLastPing - initialVelocity * timespanFromLastPing) /
                   Math.Pow(timespanFromLastPing, 2);
        }
    }
}
