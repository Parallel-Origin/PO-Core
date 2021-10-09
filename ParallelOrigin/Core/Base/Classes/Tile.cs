using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;

namespace ParallelOriginGameServer.Server.Utils {

    /// <summary>
    /// Represents a grid, other than the tile this is an absolute and short value which defines a single map grid in the world. 
    /// </summary>
    public struct Grid {
        
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
                hash = hash * 31 + x.GetHashCode();
                hash = hash * 31 + y.GetHashCode();
                return hash;
            }
        }

        public override string ToString() { return $"{nameof(x)}: {x}, {nameof(y)}: {y}"; }
    }
    
    /// <summary>
    /// Represents a map-tile ingame at a certain zoom level. 
    /// </summary>
    public struct Tile {

        public double north;
        public double south;
        public double east;
        public double west;

        public Vector2d range;
        public Vector2d middle;
        
        public bool Inside(double x, double y){ return x <= north && x >= south && y <= east && y >= west; }

        public override string ToString() { return $"{nameof(north)}: {north}, {nameof(south)}: {south}, {nameof(east)}: {east}, {nameof(west)}: {west}, {nameof(range)}: {range}, {nameof(middle)}: {middle}"; }
    }
}