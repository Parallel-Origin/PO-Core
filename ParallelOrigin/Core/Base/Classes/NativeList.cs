using System.Collections;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A wrapper for an <see cref="List" /> which is thread-safe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct NativeList<T> : IList<T>
    {
        private readonly List<T> _list;
        private readonly object _locker;

        public NativeList(int capacity)
        {
            _list = new List<T>(capacity);
            _locker = ((ICollection)_list).SyncRoot;
        }

        public int Count
        {
            get
            {
                lock (_locker)
                {
                    return _list.Count;
                }
            }
        }

        public bool IsReadOnly => ((ICollection<T>)_list).IsReadOnly;

        public void Add(T item)
        {
            lock (_locker)
            {
                _list.Add(item);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _list.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (_locker)
            {
                return _list.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_locker)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            lock (_locker)
            {
                return _list.Remove(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_locker)
            {
                return _list.GetEnumerator();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (_locker)
            {
                return ((IEnumerable<T>)_list).GetEnumerator();
            }
        }

        public T this[int index]
        {
            get
            {
                lock (_locker)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (_locker)
                {
                    _list[index] = value;
                }
            }
        }

        public int IndexOf(T item)
        {
            lock (_locker)
            {
                return _list.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (_locker)
            {
                _list.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_locker)
            {
                _list.RemoveAt(index);
            }
        }
    }
}