using System.Collections;
using System.Collections.Generic;

namespace ParallelOrigin.Core.Base.Classes;

/// <summary>
///     A wrapper for an <see cref="List" /> which is thread-safe.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct NativeList<T> : IList<T>
{
    private readonly List<T> list;
    private readonly object locker;

    public NativeList(int capacity)
    {
        list = new List<T>(capacity);
        locker = ((ICollection)list).SyncRoot;
    }

    public int Count
    {
        get
        {
            lock (locker)
            {
                return list.Count;
            }
        }
    }

    public bool IsReadOnly => ((ICollection<T>)list).IsReadOnly;

    public void Add(T item)
    {
        lock (locker)
        {
            list.Add(item);
        }
    }

    public void Clear()
    {
        lock (locker)
        {
            list.Clear();
        }
    }

    public bool Contains(T item)
    {
        lock (locker)
        {
            return list.Contains(item);
        }
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        lock (locker)
        {
            list.CopyTo(array, arrayIndex);
        }
    }

    public bool Remove(T item)
    {
        lock (locker)
        {
            return list.Remove(item);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        lock (locker)
        {
            return list.GetEnumerator();
        }
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        lock (locker)
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }
    }

    public T this[int index]
    {
        get
        {
            lock (locker)
            {
                return list[index];
            }
        }
        set
        {
            lock (locker)
            {
                list[index] = value;
            }
        }
    }

    public int IndexOf(T item)
    {
        lock (locker)
        {
            return list.IndexOf(item);
        }
    }

    public void Insert(int index, T item)
    {
        lock (locker)
        {
            list.Insert(index, item);
        }
    }

    public void RemoveAt(int index)
    {
        lock (locker)
        {
            list.RemoveAt(index);
        }
    }
}