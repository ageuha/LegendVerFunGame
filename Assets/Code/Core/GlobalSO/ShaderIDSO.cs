using UnityEngine;

namespace Code.Core.GlobalSO {
    [CreateAssetMenu(fileName = "ShaderID", menuName = "SO/GlobalData/ShaderID")]
    public class ShaderIDSO : HashSO {
        protected override void OnValidate() {
            hash = Shader.PropertyToID(parameterName);
        }

        public static implicit operator int(ShaderIDSO hash) {
            return hash.hash;
        }
    }
}