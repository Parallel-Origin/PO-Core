using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ParallelOrigin.Core.Base.Classes {

    /// <summary>
    /// Represents a grid, other than the tile this is an absolute and short value which defines a single map grid in the world. 
    /// </summary>
    public readonly struct Grid {
        
        public readonly ushort x;
        public readonly ushort y;

        public Grid(ushort x, ushort y) {
            this.x = x;
            this.y = y;
        }

        public static bool operator == (in Grid obj1, in Grid obj2) {
            return obj1.Equals(in obj2);
        }

        public static bool operator !=(in Grid obj1, in Grid obj2) {
            return !(obj1 == obj2);
        }
        
        [Pure]
        public bool Equals(in Grid other) {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj) {
            return obj is Grid other && Equals(other);
        }
        
        public override int GetHashCode() {
            unchecked {

                // Javas way of calculating hashes
                var hash = 17;
                hash = hash * 31 + x;
                hash = hash * 31 + y;
                return hash;
            }
        }

        public override string ToString() { return $"{nameof(x)}: {x}, {nameof(y)}: {y}"; }
        
        public static Grid Zero => new Grid(0, 0);
    }
    
    /// <summary>
    /// Represents a map-tile ingame at a certain zoom level. 
    /// </summary>
    public readonly struct Tile {

        public readonly double north;
        public readonly double south;
        public readonly double east;
        public readonly double west;

        public readonly Vector2d range;
        public readonly Vector2d middle;

        public Tile(double north, double south, double east, double west, Vector2d range, Vector2d middle) {
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;
            this.range = range;
            this.middle = middle;
        }

        public bool Inside(double x, double y){ return x <= north && x >= south && y <= east && y >= west; }

        public override string ToString() { return $"{nameof(north)}: {north}, {nameof(south)}: {south}, {nameof(east)}: {east}, {nameof(west)}: {west}, {nameof(range)}: {range}, {nameof(middle)}: {middle}"; }
    }
}