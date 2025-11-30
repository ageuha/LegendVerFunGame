using UnityEngine;

namespace Code.Core.GlobalSO
{
    [CreateAssetMenu(fileName = "Hash", menuName = "SO/GlobalData/Hash")]
    public class HashSO : ScriptableObject
    {
        [SerializeField] private string parameterName;
        [HideInInspector][SerializeField] private int hash;

        private void OnValidate()
        {
            hash = Animator.StringToHash(parameterName);
        }

        public static implicit operator int(HashSO hash)
        {
            return hash.hash;
        }
    }
}