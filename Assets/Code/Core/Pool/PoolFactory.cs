using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;

namespace Code.Core.Pool {
    public class PoolFactory<T> where T : MonoBehaviour, IPoolable {
        private readonly T _prefab;

        private readonly Stack<T> _pool;

        public PoolFactory(T prefab, int initialCapacity) {
            _prefab = prefab;
            _pool = new Stack<T>(initialCapacity);
        }

        public T Pop() {
            if (_pool.Count > 0) {
                var obj = _pool.Pop();
                obj.GameObject.SetActive(true);
                obj.OnPopFromPool();
                // obj.YouOut += Push;
                return obj;
            }
            else {
                var obj = Object.Instantiate(_prefab);
                obj.OnPopFromPool();
                // obj.YouOut += Push;
                return obj;
            }
        }

        public void Push(T item) {
            if (!item) {
                Logging.LogError("null을 왜 넣음?");
                return;
            }

            // obj.YouOut -= Push;
            _pool.Push(item);
            item.OnReturnToPool();
            item.GameObject.SetActive(false);
        }
    }
}