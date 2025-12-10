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
            DontDestroyOnLoad(this);
            InitializeDictionary();
        }

        private void InitializeDictionary() {
            if (_itemDictionary != null) return;
            int totalCount = 0;
            foreach (var list in itemLists) {
                totalCount += list.Prefabs.Count;
            }

            List<IPoolable> poolables = new List<IPoolable>(totalCount);

            foreach (var list in itemLists) {
                poolables.AddRange(list.Prefabs);
            }

            _itemDictionary = poolables.ToDictionary(poolable => poolable.GetType());
        }

        public PoolFactory<T> Factory<T>() where T : MonoBehaviour, IPoolable {
            if (_itemDictionary == null) InitializeDictionary();
            if (PoolFactoryContainer<T>.Factory == null) {
                if (!_itemDictionary!.TryGetValue(typeof(T), out var prefab)) {
                    Logging.LogError($"No prefab of type {typeof(T)}");
                    return null;
                }

                var factoryObj = new GameObject(typeof(T).Name + "Factory");
                factoryObj.transform.SetParent(transform);
                PoolFactoryContainer<T>.InitializeFactory(prefab as T, prefab.InitialCapacity, factoryObj.transform);
            }

            return PoolFactoryContainer<T>.Factory;
        }
    }
}