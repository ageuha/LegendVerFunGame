using Code.Core.GlobalSO;

namespace Code.EntityScripts.Interface {
    public interface IShaderController {
        void SetShaderValue(HashSO shaderID, float value);
    }
}