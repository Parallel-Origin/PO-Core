using System;
using System.Runtime.CompilerServices;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     Represents a grid, other than the tile this is an absolute and short value which defines a single map grid in the world.
    /// </summary>
    public readonly struct Grid : IEquatable<Grid>
    {
        public readonly ushort X;
        public readonly ushort Y;

        public Grid(ushort x, ushort y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(in Grid obj1, in Grid obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(in Grid obj1, in Grid obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(Grid other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Grid other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Javas way of calculating hashes
                var hash = 17;
                hash = hash * 31 + X;
                hash = hash * 31 + Y;
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }

        public static Grid Zero => new(0, 0);
    }

    /// <summary>
    ///     Represents a map-tile ingame at a certain zoom level.
    /// </summary>
    public readonly struct Tile
    {
        public readonly double North; // Higher x
        public readonly double South; // Lower x
        public readonly double East; // Lower Y
        public readonly double West; // Higher Y

        public readonly Vector2d Range;
        public readonly Vector2d Middle;

        public Tile(double north, double south, double east, double west, Vector2d range, Vector2d middle)
        {
            this.North = north;
            this.South = south;
            this.East = east;
            this.West = west;
            this.Range = range;
            this.Middle = middle;
        }

        /// <summary>
        ///     Checks wether a certain position is within the tile.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Inside(double x, double y)
        {
            return x <= North && x >= South && y >= East && y <= West;
        }

        public override string ToString()
        {
            return $"{nameof(North)}: {North}, {nameof(South)}: {South}, {nameof(East)}: {East}, {nameof(West)}: {West}, {nameof(Range)}: {Range}, {nameof(Middle)}: {Middle}";
        }
    }
}