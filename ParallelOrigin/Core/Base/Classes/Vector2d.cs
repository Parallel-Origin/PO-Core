using System;
using System.Globalization;
using System.Runtime.CompilerServices;

#if CLIENT
using Mapbox.Utils;
using UnityEngine;
#endif

namespace ParallelOrigin.Core.Base.Classes {

    [Serializable]
    public struct Vector2d {
        
        public const double kEpsilon = 1E-05d;
        public double x;
        public double y;

        public double this[int index] {
            get {
                switch (index) {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2d index!");
                }
            }
            set {
                switch (index) {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2d index!");
                }
            }
        }
        
        public Vector2d normalized {
            get {
                Vector2d vector2d = new Vector2d(this.x, this.y);
                vector2d.Normalize();
                return vector2d;
            }
        }
        
        public double magnitude {
            get {
                return Math.Sqrt(this.x * this.x + this.y * this.y);
            }
        }
        
        public double sqrMagnitude {
            get {
                return this.x * this.x + this.y * this.y;
            }
        }

        public static Vector2d zero {
            get {
                return new Vector2d(0.0d, 0.0d);
            }
        }

        public static Vector2d one {
            get {
                return new Vector2d(1d, 1d);
            }
        }

        public static Vector2d up {
            get {
                return new Vector2d(0.0d, 1d);
            }
        }

        public static Vector2d right {
            get {
                return new Vector2d(1d, 0.0d);
            }
        }

        public Vector2d(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public static Vector2d operator +(Vector2d a, Vector2d b) {
            return new Vector2d(a.x + b.x, a.y + b.y);
        }

        public static Vector2d operator -(Vector2d a, Vector2d b) {
            return new Vector2d(a.x - b.x, a.y - b.y);
        }

        public static Vector2d operator -(Vector2d a) {
            return new Vector2d(-a.x, -a.y);
        }

        public static Vector2d operator *(Vector2d a, double d) {
            return new Vector2d(a.x * d, a.y * d);
        }

        public static Vector2d operator *(float d, Vector2d a) {
            return new Vector2d(a.x * d, a.y * d);
        }

        public static Vector2d operator /(Vector2d a, double d) {
            return new Vector2d(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2d lhs, Vector2d rhs) {
            return Vector2d.SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
        }

        public static bool operator !=(Vector2d lhs, Vector2d rhs) {
            return (double)Vector2d.SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
        }

#if UNITY_2020 || UNITY_2021
        
        public static explicit operator Vector2d(Mapbox.Utils.Vector2d content) {
            return new Vector2d {
                x = content.x,
                y = content.y
            };
        }
        
        public static explicit operator Mapbox.Utils.Vector2d(Vector2d content) {
            return new Mapbox.Utils.Vector2d {
                x = content.x,
                y = content.y
            };
        }
        
#endif

        public void Set(double new_x, double new_y) {
            this.x = new_x;
            this.y = new_y;
        }

        public static Vector2d Lerp(Vector2d from, Vector2d to, double t) {
            t = t;
            return new Vector2d(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }

        public static Vector2d MoveTowards(Vector2d current, Vector2d target, double maxDistanceDelta) {
            Vector2d vector2 = target - current;
            double magnitude = vector2.magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0.0d)
                return target;
            else
                return current + vector2 / magnitude * maxDistanceDelta;
        }

        public static Vector2d Scale(Vector2d a, Vector2d b) {
            return new Vector2d(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2d scale) {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        public void Normalize() {
            double magnitude = this.magnitude;
            if (magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Vector2d.zero;
        }

        public override string ToString() {
            return string.Format(NumberFormatInfo.InvariantInfo, "{0:F5},{1:F5}", this.y, this.x);
        }

        public override int GetHashCode() {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other) {
            if (!(other is Vector2d))
                return false;
            Vector2d vector2d = (Vector2d)other;
            if (this.x.Equals(vector2d.x))
                return this.y.Equals(vector2d.y);
            else
                return false;
        }

        public static double Dot(Vector2d lhs, Vector2d rhs) {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public static double Angle(Vector2d from, Vector2d to) {
#if SERVER
            return Math.Acos(Math.Clamp(Vector2d.Dot(from.normalized, to.normalized), -1d, 1d)) * 57.29578d;
#else
            return Mathd.Acos(Mathd.Clamp(Vector2d.Dot(from.normalized, to.normalized), -1d, 1d)) * 57.29578d;
#endif
        }

        public static double Distance(Vector2d a, Vector2d b) {
            return (a - b).magnitude;
        }

        public static Vector2d ClampMagnitude(Vector2d vector, double maxLength) {
            if (vector.sqrMagnitude > maxLength * maxLength)
                return vector.normalized * maxLength;
            else
                return vector;
        }

        public static double SqrMagnitude(Vector2d a) {
            return (a.x * a.x + a.y * a.y);
        }

        public double SqrMagnitude() {
            return (this.x * this.x + this.y * this.y);
        }

        public static Vector2d Min(Vector2d lhs, Vector2d rhs) {
            return new Vector2d(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y));
        }

        public static Vector2d Max(Vector2d lhs, Vector2d rhs) {
            return new Vector2d(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y));
        }

        public double[] ToArray() {
            double[] array =
            {
                    this.x,
                    this.y
                };

            return array;
        }
    }

    /// <summary>
    /// An extension for the <see cref="Vector2d"/>
    /// </summary>
    public static class Vector2dExtensions {
        
        /// <summary>
        /// Makes one <see cref="Vector2d"/> move towards another <see cref="Vector2d"/>
        /// </summary>
        /// <param name="current">The vec we wanna move to the target</param>
        /// <param name="target">The target we wanna move to, is readonly... ref only used because in will copy if its not a readonly struct</param>
        /// <param name="maxDistanceDelta"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MoveTowards(this ref Vector2d current, ref Vector2d target, in float maxDistanceDelta) {
            
            var vector2 = target - current;
            var magnitude = vector2.magnitude;
            
            if (magnitude <= maxDistanceDelta || magnitude == 0.0d) return false;
             
            current += vector2 / magnitude * maxDistanceDelta;
            return true;
        }
    }
}