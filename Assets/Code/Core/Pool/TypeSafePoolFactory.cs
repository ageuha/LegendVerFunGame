using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core.Pool {
    public class TypeSafePoolFactory<T> where T : MonoBehaviour, IPoolable {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<T> _pool;

        public TypeSafePoolFactory(T prefab, int initialCapacity, Transform parent) {
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

    public class DynamicPoolFactory {
        private readonly MonoBehaviour _prefab;
        private readonly Transform _parent;
        private readonly Stack<MonoBehaviour> _pool;

        public DynamicPoolFactory(MonoBehaviour prefab, int initialCapacity, Transform parent) {
            _prefab = prefab;
            _parent = parent;
            _pool = new Stack<MonoBehaviour>(initialCapacity);
        }

        public MonoBehaviour Pop(Transform parent = null) {
            MonoBehaviour obj;
            if (_pool.Count > 0) {
                obj = _pool.Pop();
                obj.gameObject.SetActive(true);
            }
            else {
                obj = Object.Instantiate(_prefab);
            }

            SetObjectParent(parent, obj);
            if (obj is IPoolable poolable)
                poolable.OnPopFromPool();
            // obj.YouOut += Push;
            return obj;
        }

        private static void SetObjectParent(Transform parent, MonoBehaviour obj) {
            obj.transform.SetParent(parent);
            if (!parent)
                SceneManager.MoveGameObjectToScene(obj.gameObject, SceneManager.GetActiveScene());
        }

        public void Push(MonoBehaviour item) {
            if (!item) {
                Logging.LogError("null을 왜 넣음?");
                return;
            }

            // obj.YouOut -= Push;
            _pool.Push(item);
            if (item is IPoolable poolable)
                poolable.OnReturnToPool();
            item.gameObject.SetActive(false);
            item.transform.SetParent(_parent);
        }
    }
}