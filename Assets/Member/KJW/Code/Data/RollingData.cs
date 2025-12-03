using UnityEngine;

namespace Member.KJW.Code.Data
{
    [CreateAssetMenu(fileName = "RollingData", menuName = "SO/RollingData")]
    public class RollingData : ScriptableObject
    {
        [field: SerializeField, Tooltip("구르기 세기")] public float RollPower { get; private set; }
        [field: SerializeField, Tooltip("구르기 시간")] public float RollTime { get; private set; }
        [field: SerializeField, Tooltip("구르기 후딜")] public float AfterDelayTime { get; private set; }
        [field: SerializeField, Tooltip("최대 구르기 개수")] public int MaxRoll { get; private set; }
        [field: SerializeField, Tooltip("구르기 충전 시간")] public float StackCoolTime { get; private set; }
    }
}