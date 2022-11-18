using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ParallelOrigin.Core.Base.Classes {
    /// <summary>
    ///     A wrapped <see cref="Dictionary" /> which contains several events to execute logic once a kvp was added to the dictionary or was removed.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class EventDictionary<K, V> : IDictionary<K, V>
    {
        public EventDictionary()
        {
            Dictionary = new Dictionary<K, V>(4);
        }

        public EventDictionary(int size)
        {
            Dictionary = new Dictionary<K, V>(size);
        }

        public Dictionary<K, V> Dictionary { get; set; }
        public Action<K, V> OnAdded { get; set; }
        public Action<K, V> OnRemoved { get; set; }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return Dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return Remove(item.Key);
        }

        public int Count => Dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            Dictionary.Add(key, value);
            OnAdded(key, value);
        }

        public bool ContainsKey(K key)
        {
            return Dictionary.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            OnRemoved(key, Dictionary[key]);
            return Dictionary.Remove(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public V this[K key]
        {
            get => Dictionary[key];
            set
            {
                Dictionary[key] = value;
                OnAdded(key, value);
            }
        }

        public ICollection<K> Keys => Dictionary.Keys;
        public ICollection<V> Values => Dictionary.Values;
    }
}