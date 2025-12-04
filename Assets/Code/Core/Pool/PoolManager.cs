using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;

namespace Code.Core.Pool {
    public class PoolManager : MonoSingleton<PoolManager> {
        [SerializeField] private PoolItemList items;
        [SerializeField] private int initialCapacity = 10;

        private Dictionary<Type, IPoolable> _itemDictionary;

        protected override void Awake() {
            base.Awake();
            InitializeDictionary();
        }

        private void InitializeDictionary() {
            _itemDictionary ??= items.Prefabs.ToDictionary(poolable => poolable.GetType());
        }

        public PoolFactory<T> Factory<T>() where T : MonoBehaviour, IPoolable {
            if (_itemDictionary == null) InitializeDictionary();
            if (PoolFactoryContainer<T>.Factory == null) {
                if (!_itemDictionary!.TryGetValue(typeof(T), out var prefab)) {
                    Logging.LogError($"No prefab of type {typeof(T)}");
                    return null;
                }

                PoolFactoryContainer<T>.InitializeFactory(prefab as T, initialCapacity);
            }

            return PoolFactoryContainer<T>.Factory;
        }
    }
}