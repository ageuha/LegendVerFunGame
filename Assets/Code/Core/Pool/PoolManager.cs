// using System;
// using System.Collections.Generic;
// using System.Linq;
// using MemberFolder.YD.Code.Core.Utility;
// using UnityEngine;
//
// namespace MemberFolder.YD.Code.Core.Pool {
//     public class PoolManager : MonoSingleton<PoolManager> {
//         [SerializeField] private PoolPrefabList prefabs;
//         [SerializeField] private int initialCapacity = 10;
//         
//         private Dictionary<Type, PoolableObject> _prefabDictionary;
//
//         protected override void Awake() {
//             base.Awake();
//             InitializeDictionary();
//         }
//
//         private void InitializeDictionary() {
//             _prefabDictionary = prefabs.Prefabs.ToDictionary(prefab => prefab.GetType());
//         }
//         
//         public PoolFactory<T> Factory<T>() where T : PoolableObject {
//             if (PoolFactoryContainer<T>.Factory == null) {
//                 if (!_prefabDictionary.TryGetValue(typeof(T), out var prefab)) {
//                     Logging.LogError($"No prefab of type {typeof(T)}");
//                     return null;
//                 }
//                 PoolFactoryContainer<T>.InitializeFactory(prefab as T, initialCapacity);
//             }
//             return PoolFactoryContainer<T>.Factory;
//         }
//     }
// }