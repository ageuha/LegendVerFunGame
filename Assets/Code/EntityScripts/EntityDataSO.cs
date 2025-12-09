using UnityEngine;

namespace Code.EntityScripts {
    [CreateAssetMenu(fileName = "new EntityData", menuName = "SO/EntityData", order = 0)]
    public class EntityDataSO : ScriptableObject {
        [field: SerializeField] public float MaxHp { get; private set; }
        
    }
}