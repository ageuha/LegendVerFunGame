using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Utility {
    /// <summary>
    /// 이거 진짜 Json직렬화 용이니까, Dictionary 대체로 쓰면 안됨.
    /// <para>콘크리트 클래스 만드는 건 기본인 거 알지?</para>
    /// </summary>
    /// <typeparam name="TKey">Key타입</typeparam>
    /// <typeparam name="TValue">Value타입</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> {
        [SerializeField] private List<TKey> keys;
        [SerializeField] private List<TValue> values;

        private Dictionary<TKey, TValue> _dict;

        public SerializableDictionary() {
            keys = new List<TKey>();
            values = new List<TValue>();
        }

        public SerializableDictionary(int capacity) {
            keys = new List<TKey>(capacity);
            values = new List<TValue>(capacity);
        }

        public SerializableDictionary(Dictionary<TKey, TValue> dict) {
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