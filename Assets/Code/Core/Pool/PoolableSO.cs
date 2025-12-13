using UnityEngine;

namespace Code.Core.Pool {
    [CreateAssetMenu(fileName = "new Poolable", menuName = "SO/Pool/PoolableItem", order = 0)]
    public class PoolableSO : ScriptableObject {
        [field: SerializeField] public MonoBehaviour Prefab { get; private set; }
    }
}