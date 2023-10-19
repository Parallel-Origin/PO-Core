using System;
using System.Globalization;
using System.Runtime.CompilerServices;
#if CLIENT
using Mapbox.Utils;
using UnityEngine;
#endif

namespace ParallelOrigin.Core.Base.Classes
{
    [Serializable]
    public struct Vector2d
    {
        public const double K_EPSILON = 1E-05d;
        public double X;
        public double Y;
        
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2d index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2d index!");
                }
            }
        }
        
        public Vector2d Normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var vector2d = new Vector2d(X, Y);
                vector2d.Normalize();
                return vector2d;
            }
        }

        public double Magnitude => Math.Sqrt(X * X + Y * Y);
        
        public static Vector2d Zero => new Vector2d(0.0d, 0.0d);

        public static Vector2d One => new Vector2d(1d, 1d);

        public static Vector2d Up => new Vector2d(0.0d, 1d);

        public static Vector2d Right => new Vector2d(1d, 0.0d);

        public Vector2d(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2d operator +(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2d operator -(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2d operator -(Vector2d a)
        {
            return new Vector2d(-a.X, -a.Y);
        }

        public static Vector2d operator *(Vector2d a, double d)
        {
            return new Vector2d(a.X * d, a.Y * d);
        }

        public static Vector2d operator *(float d, Vector2d a)
        {
            return new Vector2d(a.X * d, a.Y * d);
        }

        public static Vector2d operator /(Vector2d a, double d)
        {
            return new Vector2d(a.X / d, a.Y / d);
        }

        public static bool operator ==(Vector2d lhs, Vector2d rhs)
        {
            return SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
        }

        public static bool operator !=(Vector2d lhs, Vector2d rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
        }

#if UNITY_2020 || UNITY_2021
        public static explicit operator Vector2d(Mapbox.Utils.Vector2d content) {
            return new Vector2d {
                X = content.x,
                Y = content.y
            };
        }
        
        public static explicit operator Mapbox.Utils.Vector2d(Vector2d content) {
            return new Mapbox.Utils.Vector2d {
                x = content.X,
                y = content.Y
            };
        }

#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(double newX, double newY)
        {
            X = newX;
            Y = newY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d Lerp(Vector2d from, Vector2d to, double t)
        {
            t = t;
            return new Vector2d(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d MoveTowards(Vector2d current, Vector2d target, double maxDistanceDelta)
        {
            var vector2 = target - current;
            var magnitude = vector2.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0.0d)
                return target;
            return current + vector2 / magnitude * maxDistanceDelta;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d Scale(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X * b.X, a.Y * b.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector2d scale)
        {
            X *= scale.X;
            Y *= scale.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            var magnitude = this.Magnitude;
            if (magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Zero;
        }

        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "{0:F5},{1:F5}", Y, X);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ (Y.GetHashCode() << 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2d))
                return false;
            var vector2d = (Vector2d)other;
            if (X.Equals(vector2d.X))
                return Y.Equals(vector2d.Y);
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Dot(Vector2d lhs, Vector2d rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Angle(Vector2d from, Vector2d to)
        {
#if SERVER
            return Math.Acos(Math.Clamp(Dot(from.Normalized, to.Normalized), -1d, 1d)) * 57.29578d;
#else
            return Mathd.Acos(Mathd.Clamp(Vector2d.Dot(from.Normalized, to.Normalized), -1d, 1d)) * 57.29578d;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Distance(Vector2d a, Vector2d b)
        {
            return (a - b).Magnitude;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d ClampMagnitude(Vector2d vector, double maxLength)
        {
            if (SqrMagnitude(vector) > maxLength * maxLength)
                return vector.Normalized * maxLength;
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SqrMagnitude(Vector2d a)
        {
            return a.X * a.X + a.Y * a.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d Min(Vector2d lhs, Vector2d rhs)
        {
            return new Vector2d(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y));
            
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2d Max(Vector2d lhs, Vector2d rhs)
        {
            return new Vector2d(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y));
        }

        public double[] ToArray()
        {
            double[] array =
            {
                X,
                Y
            };

            return array;
        }
    }

    /// <summary>
    ///     An extension for the <see cref="Vector2d" />
    /// </summary>
    public static class Vector2dExtensions
    {
        /// <summary>
        ///     Makes one <see cref="Vector2d" /> move towards another <see cref="Vector2d" />
        /// </summary>
        /// <param name="current">The vec we wanna move to the target</param>
        /// <param name="target">The target we wanna move to, is readonly... ref only used because in will copy if its not a readonly struct</param>
        /// <param name="maxDistanceDelta"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool MoveTowards(this ref Vector2d current, ref Vector2d target, in float maxDistanceDelta)
        {
            var vector2 = target - current;
            var magnitude = vector2.Magnitude;

            if (magnitude <= maxDistanceDelta || magnitude == 0.0d) return false;

            current += vector2 / magnitude * maxDistanceDelta;
            return true;
        }
    }
}