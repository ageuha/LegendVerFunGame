using System;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetMoveInputAction", story: "Set [Mover] MoveInput to [Value]", category: "Move", id: "c84df7933bcf59e7e1a1ea0adf630a6d")]
    public partial class SetMoveInputAction : Action {
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Vector2> Value;
        protected override Status OnStart() {
            if (!Mover.Value) {
                Logging.LogError("SetMoveInputAction에 Mover 참조 까먹으셨어요");
                return Status.Failure;
            }

            Mover.Value.SetMovementInput(Value.Value);
            return Status.Success;
        }
    }
}