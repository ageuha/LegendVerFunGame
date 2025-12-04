using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
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

        private void OnValidate()
        {
            for (var i = 0; i < prefabs.Count; i++)
            {
                var prefab = prefabs[i];
                if (prefab == null) continue;
                if (prefab is IPoolable)
                    continue;
                if (prefab.TryGetComponent(out IPoolable poolable))
                {
                 prefabs[i] = poolable as MonoBehaviour;
                 continue;
                }

                prefabs.RemoveAt(i);
                Logging.LogError("IPoolable이 아닌 객체가 있습니다.");
            }
        }
    }
}