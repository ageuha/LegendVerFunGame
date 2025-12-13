using System.Collections.Generic;
using UnityEngine;

namespace Code.Core.Pool {
    [CreateAssetMenu(fileName = "new PoolItemList", menuName = "SO/Pool/DynamicPoolItemList", order = 0)]
    public class DynamicPoolItemList : ScriptableObject {
        [field: SerializeField] public List<PoolableSO> PoolList { get; private set; }
    }
}