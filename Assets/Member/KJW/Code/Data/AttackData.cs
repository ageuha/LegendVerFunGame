using UnityEngine;

namespace Member.KJW.Code.Data
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/AttackData", order = 0)]
    public class AttackData : ScriptableObject
    {
        [field: SerializeField] public DamageInfoData DamageInfoData { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        
    }
}