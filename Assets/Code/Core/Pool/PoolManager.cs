using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;

namespace Code.Core.Pool {
    public class PoolManager : MonoSingleton<PoolManager> {
        [SerializeField] private List<PoolItemList> itemLists;
        [SerializeField] private List<DynamicPoolItemList> dynamicPoolItemLists;

        private Dictionary<Type, IPoolable> _itemDictionary;
        private Dictionary<PoolableSO, DynamicPoolFactory> _factoryDictionary;

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

            if (_factoryDictionary != null) return;
            int totalCount2 = 0;
            foreach (var list in dynamicPoolItemLists) {
                totalCount2 += list.PoolList.Count;
            }

            List<PoolableSO> poolables2 = new List<PoolableSO>(totalCount2);

            foreach (var list in dynamicPoolItemLists) {
                poolables2.AddRange(list.PoolList);
            }

            var factoryObj = new GameObject("DynamicFactory");
            factoryObj.transform.SetParent(transform);
            _factoryDictionary = poolables2.ToDictionary(poolable => poolable,
                poolable => new DynamicPoolFactory(poolable.Prefab, 8, factoryObj.transform));
        }

        public TypeSafePoolFactory<T> Factory<T>() where T : MonoBehaviour, IPoolable {
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

        public DynamicPoolFactory DynamicFactory(PoolableSO so) {
            if (_factoryDictionary.TryGetValue(so, out var factory)) return factory;
            Logging.LogError("List에 존재하지 않는 PoolableSO입니다.");
            return null;
        }
    }
}