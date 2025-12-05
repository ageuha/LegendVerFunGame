using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;

namespace Code.Core.Pool {
    public class PoolManager : MonoSingleton<PoolManager> {
        [SerializeField] private List<PoolItemList> itemLists;

        private Dictionary<Type, IPoolable> _itemDictionary;

        protected override void Awake() {
            base.Awake();
            InitializeDictionary();
        }

        private void InitializeDictionary() {
            int totalCount = 0;
            foreach (var list in itemLists) {
                totalCount += list.Prefabs.Count;
            }

            List<IPoolable> poolables = new List<IPoolable>(totalCount);

            foreach (var list in itemLists) {
                poolables.AddRange(list.Prefabs);
            }

            _itemDictionary ??= poolables.ToDictionary(poolable => poolable.GetType());
        }

        public PoolFactory<T> Factory<T>() where T : MonoBehaviour, IPoolable {
            if (_itemDictionary == null) InitializeDictionary();
            if (PoolFactoryContainer<T>.Factory == null) {
                if (!_itemDictionary!.TryGetValue(typeof(T), out var prefab)) {
                    Logging.LogError($"No prefab of type {typeof(T)}");
                    return null;
                }

                PoolFactoryContainer<T>.InitializeFactory(prefab as T, prefab.InitialCapacity);
            }

            return PoolFactoryContainer<T>.Factory;
        }
    }
}