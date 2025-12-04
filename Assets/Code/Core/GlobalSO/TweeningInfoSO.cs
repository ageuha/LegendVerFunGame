using DG.Tweening;
using UnityEngine;

namespace Code.Core.GlobalSO {
    [CreateAssetMenu(fileName = "new TweeningInfo", menuName = "SO/GlobalSO/TweeningInfo", order = 0)]
    public class TweeningInfoSO : ScriptableObject {
        [field: SerializeField] public Ease EasingType { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        
    }
}