using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Utility {
    /// <summary>
    /// 이거 진짜 Json직렬화 용이니까, Dictionary 대체로 쓰면 안됨.
    /// <para>+ 콘크리트 클래스 만들어야 직렬화 가능</para>
    /// </summary>
    /// <typeparam name="TKey">Key타입</typeparam>
    /// <typeparam name="TValue">Value타입</typeparam>
    [Serializable]
    public abstract class SerializableDictionary<TKey, TValue> {
        [SerializeField] private List<TKey> keys;
        [SerializeField] private List<TValue> values;

        private Dictionary<TKey, TValue> _dict;

        protected SerializableDictionary() {
            keys = new List<TKey>();
            values = new List<TValue>();
        }

        protected SerializableDictionary(int capacity) {
            keys = new List<TKey>(capacity);
            values = new List<TValue>(capacity);
        }

        protected SerializableDictionary(Dictionary<TKey, TValue> dict) {
            keys = new List<TKey>(dict.Keys);
            values = new List<TValue>(dict.Values);
        }

        public Dictionary<TKey, TValue> ToDictionary() {
            if (_dict != null) return _dict;

            _dict = new Dictionary<TKey, TValue>();
            for (int i = 0; i < keys.Count; i++)
                _dict[keys[i]] = values[i];

            return _dict;
        }
    }
}