using System;

namespace Warehouse
{
    /// <summary>
    ///  An (x, y) coordinate in a warehouse.
    /// </summary>
    public struct Position : IEquatable<Position>
    {
        /// <summary>
        /// The x coordinate of the position.
        /// </summary>
        public double X { get; }
        
        /// <summary>
        /// The y coordinate of the position.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Creates a new position with the given coordinates.
        /// </summary>
        /// <param name="x">The x coordinate of the position.</param>
        /// <param name="y">The y coordinate of the position.</param>
        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Computes the hash code of the position.
        /// </summary>
        /// <returns>A hash code consistent with equality.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = hash * 31 + X.GetHashCode();
                hash = hash * 31 + Y.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Compares this position with another object for equality.
        /// </summary>
        /// <param name="obj">The object to compare this position with.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is another <see cref="Position"/> equal to this one; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj) =>
            obj is Position && Equals((Position) obj);

        /// <summary>
        /// Compares this position with another for equality.
        /// </summary>
        /// <param name="other">The position to compare.</param>
        /// <returns><c>true</c> if the two positions have exactly equal coordinates; <c>false</c> otherwise.</returns>
        public bool Equals(Position other) =>
            X == other.X && Y == other.Y;

        /// <summary>
        /// Determines the distance between two positions as the Euclidean distance in two
        /// dimensions.
        /// </summary>
        /// <param name="position1">The first position.</param>
        /// <param name="position2">The second position.</param>
        /// <returns>The Euclidean distance between the two positions.</returns>
        public static double GetDistance(Position position1, Position position2)
        {
            double xDiff = Math.Abs(position1.X - position2.X);
            double yDiff = Math.Abs(position1.Y - position2.Y);
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        /// <summary>
        /// Returns a string representation of the position.
        /// </summary>
        /// <returns>A string representation of the position.</returns>
        public override string ToString() => $"({X}, {Y})";
    }
}
