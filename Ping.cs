using System;

namespace Warehouse
{
    /// <summary>
    /// A Ping represents a vehicle's position at a given timestamp.
    /// </summary>
    public sealed class Ping
    {
        private const double Tolerance = 0.1;
        
        /// <summary>
        /// The position of the vehicle.
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// The timestamp at which the vehicle was at <see cref="Position" />, in seconds
        /// since a fixed (but arbitrary) epoch.
        /// </summary>
        public long Timestamp { get; }

        /// <summary>
        /// Creates a new ping for the given position and timestamp.
        /// </summary>
        /// <param name="x">X coordinate of the position</param>
        /// <param name="y">Y coordinate of the position</param>
        /// <param name="timestamp">Timestamp of the ping, in seconds since the epoch.</param>
        public Ping(double x, double y, long timestamp)
        {
            Position = new Position(x, y);
            Timestamp = timestamp;
        }

        /// <summary>
        /// Returns a string representation of the ping.
        /// </summary>
        /// <returns>A string representation of the ping.</returns>
        public override string ToString() => $"{Position} @ {Timestamp}";

        /// <summary>
        /// Determines the number of seconds between two given pings.
        /// </summary>
        /// <param name="ping1">The first ping.</param>
        /// <param name="ping2">The second ping.</param>
        /// <returns>The difference between the timestamps of the pings, in seconds.
        /// The result is positive if ping1 is earlier than ping2.</returns>
        public static long SecondsBetween(Ping ping1, Ping ping2) =>
            ping2.Timestamp - ping1.Timestamp;
        
        /// <summary>
        /// Determines the distance between two given pings.
        /// </summary>
        /// <param name="ping1">The first ping.</param>
        /// <param name="ping2">The second ping.</param>
        /// <returns>The distance between two pings.</returns>
        public static double CalculateDistance(Ping ping1, Ping ping2)
        {
            var x1 = ping1.Position.X;
            var y1 = ping1.Position.Y;
            var x2 = ping2.Position.X;
            var y2 = ping2.Position.Y;
            var distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            
            return distance;
        }

        public override bool Equals(object obj)
        {
            return obj != null
                && obj is Ping ping
                && Math.Abs(ping.Position.X - Position.X) < Tolerance
                && Math.Abs(ping.Position.Y - Position.Y) < Tolerance
                && ping.Timestamp == Timestamp;
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            var hCode = Position.X * Position.Y * Timestamp;
            return hCode.GetHashCode();
        }
    }
}