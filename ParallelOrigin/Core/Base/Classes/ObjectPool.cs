using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A pool of objects which can be used to pool items for short lived operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ObjectPool<T> where T : new()
    {
        private static readonly ConcurrentBag<T> _objects = new();

        /// <summary>
        ///     Gets an object from the pool or creates a new one
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get()
        {
            if (_objects.TryTake(out var item)) return item;

            var newInstance = new T();
            return newInstance;
        }

        /// <summary>
        ///     Returns an object to the pool which results in being added
        /// </summary>
        /// <param name="item"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Return(T item)
        {
            _objects.Add(item);
        }
    }
}