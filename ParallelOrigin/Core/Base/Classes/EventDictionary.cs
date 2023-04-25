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
    public class EventDictionary<TK, TV> : IDictionary<TK, TV>
    {
        public EventDictionary()
        {
            Dictionary = new Dictionary<TK, TV>(4);
        }

        public EventDictionary(int size)
        {
            Dictionary = new Dictionary<TK, TV>(size);
        }

        public Dictionary<TK, TV> Dictionary { get; set; }
        public Action<TK, TV> OnAdded { get; set; }
        public Action<TK, TV> OnRemoved { get; set; }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TK, TV> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TK, TV> item)
        {
            return Dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TK, TV> item)
        {
            return Remove(item.Key);
        }

        public int Count => Dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(TK key, TV value)
        {
            Dictionary.Add(key, value);
            OnAdded(key, value);
        }

        public bool ContainsKey(TK key)
        {
            return Dictionary.ContainsKey(key);
        }

        public bool Remove(TK key)
        {
            OnRemoved(key, Dictionary[key]);
            return Dictionary.Remove(key);
        }

        public bool TryGetValue(TK key, out TV value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public TV this[TK key]
        {
            get => Dictionary[key];
            set
            {
                Dictionary[key] = value;
                OnAdded(key, value);
            }
        }

        public ICollection<TK> Keys => Dictionary.Keys;
        public ICollection<TV> Values => Dictionary.Values;
    }
}