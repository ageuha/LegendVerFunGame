using UnityEngine;

namespace _01._SO.Move
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "SO/MovementData")]
    public class MovementData : ScriptableObject
    {
        /// <summary>
        /// 가속
        /// </summary>
        [field: SerializeField] public float Acceleration { get; private set; }
        /// <summary>
        /// 감속
        /// </summary>
        [field: SerializeField] public float Deacceleration { get; private set; }
        /// <summary>
        /// 최고 속도
        /// </summary>
        [field: SerializeField] public float MaxSpeed { get; private set; }
        /// <summary>
        /// 달리기 계수
        /// </summary>
        [field: SerializeField] public float DashMultiplyValue { get; private set; }
    }
}