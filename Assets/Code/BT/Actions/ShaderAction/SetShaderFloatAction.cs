using System;
using Code.Core.GlobalSO;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.ShaderAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetShaderFloat", story: "Set [Shader] [Hash] [Value]", category: "Action/Animation",
        id: "cafefb0db1dd08fd4caae0ccc97629f8")]
    public partial class SetShaderFloatAction : Action {
        [SerializeReference] public BlackboardVariable<EntityRenderer> Shader;
        [SerializeReference] public BlackboardVariable<HashSO> Hash;
        [SerializeReference] public BlackboardVariable<float> Value;

        protected override Status OnStart() {
            if (!Shader.Value || !Hash.Value) {
                Logging.LogError("SetShaderFloatAction에서 뭔가가 null임");
                return Status.Failure;
            }

            Shader.Value.SetShaderValue(Hash.Value, Value.Value);

            return Status.Success;
        }
    }
}