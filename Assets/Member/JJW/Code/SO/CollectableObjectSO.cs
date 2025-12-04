using UnityEngine;

namespace Member.JJW.Code.SO
{
    [CreateAssetMenu(fileName = "CollectableObjectSO", menuName = "JJW/CollectableObjectSO", order = 0)]
    public class CollectableObjectSO : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public string Description;
    }
}