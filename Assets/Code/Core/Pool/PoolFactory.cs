using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Pool {
    public class PoolFactory<T> where T : PoolableObject
    {
        private readonly T _prefab;

        private readonly Stack<T> _pool;

        public PoolFactory(T prefab, int initialCapacity)
        {
            _prefab = prefab;
            _pool = new Stack<T>(initialCapacity);
        }

        public T Pop()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Pop();
                obj.gameObject.SetActive(true);
                obj.OnPopFromPool();
                obj.YouOut += Push;
                return obj;
            }
            else
            {
                var obj =  Object.Instantiate(_prefab);
                obj.OnPopFromPool();
                obj.YouOut += Push;
                return obj;
            }
        }

        public void Push(PoolableObject item)
        {
            if(item is not T obj)
            {
                Debug.LogError($"Trying to push object of type {item.GetType()} into pool of type {typeof(T)}");
                return;
            }
            obj.YouOut -= Push;
            _pool.Push(obj);
            obj.OnReturnToPool();
            obj.gameObject.SetActive(false);
        }
    }
}
