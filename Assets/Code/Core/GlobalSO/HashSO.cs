#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Code.Core.GlobalSO {
    [CreateAssetMenu(fileName = "Hash", menuName = "SO/GlobalData/Hash")]
    public class HashSO : ScriptableObject {
        [SerializeField] protected string parameterName;
        [HideInInspector] [SerializeField] protected int hash;

        protected virtual void OnValidate() {
            hash = Animator.StringToHash(parameterName);
        }

        public void SetName() {
#if UNITY_EDITOR
            if (!string.IsNullOrEmpty(parameterName) && name != parameterName) {
                string assetPath = AssetDatabase.GetAssetPath(this as Object);
                if (!string.IsNullOrEmpty(assetPath)) {
                    AssetDatabase.RenameAsset(assetPath, parameterName);
                    AssetDatabase.SaveAssets();
                    EditorUtility.SetDirty(this);
                    name = parameterName;
                }
            }

#endif
        }

        public static implicit operator int(HashSO hash) {
            return hash.hash;
        }
    }
}