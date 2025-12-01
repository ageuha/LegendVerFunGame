using UnityEngine;

namespace KJW.Code.Data
{
    [CreateAssetMenu(fileName = "RollingData", menuName = "SO/RollingData")]
    public class RollingData : ScriptableObject
    {
        [field: SerializeField] public float RollPower { get; private set; }
        [field: SerializeField] public float RollTime { get; private set; }
    }
}