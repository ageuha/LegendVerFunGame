using System;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetMoveSpeed", story: "Set [Mover] Speed [Value]", category: "Move",
        id: "c5c74416152f137e46eac1abd184c46a")]
    public partial class SetMoveSpeedAction : Action {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;
        [SerializeReference] public BlackboardVariable<float> Value;

        protected override Status OnStart() {
            if (!Mover.Value) {
                Logging.LogError("Mover가 null이야 ㅠㅠㅠ");
                return Status.Failure;
            }

            Mover.Value.SetSpeed(Value.Value);
            return Status.Success;
        }
    }
}