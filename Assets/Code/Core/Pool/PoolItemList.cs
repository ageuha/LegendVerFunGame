using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Core.Pool {
    [CreateAssetMenu(fileName = "PrefabList", menuName = "SO/Pool/PrefabList", order = 0)]
    public class PoolItemList : ScriptableObject {
        [SerializeField] private List<MonoBehaviour> prefabs;

        public IReadOnlyList<IPoolable> Prefabs {
            get {
                List<IPoolable> poolables = prefabs.Select(prefab => prefab as IPoolable).ToList();
                return poolables;
            }
        }

        private void OnValidate() {
            prefabs.RemoveAll(prefab => prefab is not IPoolable);
        }
    }
}