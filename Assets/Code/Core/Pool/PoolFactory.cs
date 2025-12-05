using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core.Pool {
    public class PoolFactory<T> where T : MonoBehaviour, IPoolable {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _pool;

        public PoolFactory(T prefab, int initialCapacity, Transform parent) {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<T>(initialCapacity);
        }

        public T Pop(Transform parent = null) {
            T obj;
            if (_pool.Count > 0) {
                obj = _pool.Pop();
                obj.gameObject.SetActive(true);
            }
            else {
                obj = Object.Instantiate(_prefab);
            }

            SetObjectParent(parent, obj);
            obj.OnPopFromPool();
            // obj.YouOut += Push;
            return obj;
        }

        private static void SetObjectParent(Transform parent, T obj) {
            obj.transform.SetParent(parent);
            if (!parent)
                SceneManager.MoveGameObjectToScene(obj.gameObject, SceneManager.GetActiveScene());
        }

        public void Push(T item) {
            if (!item) {
                Logging.LogError("null을 왜 넣음?");
                return;
            }

            // obj.YouOut -= Push;
            _pool.Push(item);
            item.OnReturnToPool();
            item.gameObject.SetActive(false);
            item.transform.SetParent(_parent);
        }
    }
}