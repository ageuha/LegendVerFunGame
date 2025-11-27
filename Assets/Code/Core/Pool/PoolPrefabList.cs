using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Pool {
    [CreateAssetMenu(fileName = "PrefabList", menuName = "SO/Pool/PrefabList", order = 0)]
    public class PoolPrefabList : ScriptableObject {
        [SerializeField] private List<PoolableObject> prefabs;
        public IReadOnlyList<PoolableObject> Prefabs => prefabs;
    }
}