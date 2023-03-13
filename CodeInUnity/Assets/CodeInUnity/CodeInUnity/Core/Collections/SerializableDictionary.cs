using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeInUnity.Core.Collections
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        public TValue this[TKey key]
        {
            get
            {
                if (this.dictionary.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                return default(TValue);
            }

            set
            {
                dictionary[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return this.keys.ToArray();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return this.values.ToArray();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Clear()
        {
            this.dictionary.Clear();
        }

        public void Add(TKey key, TValue val)
        {
            this.dictionary.Add(key, val);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.dictionary.Clear();

            for (int i = 0; i < keys.Count && i < values.Count; i++)
            {
                this.dictionary.Add(keys[i], values[i]);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.keys.Clear();
            this.values.Clear();

            foreach (var kv in dictionary)
            {
                this.keys.Add(kv.Key);
                this.values.Add(kv.Value);
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        public bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return this.dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.dictionary[item.Key] = item.Value;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.dictionary.Remove(item.Key);
        }
    }
}