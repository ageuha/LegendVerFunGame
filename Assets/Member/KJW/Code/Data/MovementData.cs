using UnityEngine;

namespace Member.KJW.Code.Data
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "SO/MovementData")]
    public class MovementData : ScriptableObject
    {
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float Deacceleration { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
    }
}