using System;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "CalculateDirection", story: "Calculate [Direction] [Self] to [NextPosition]",
        category: "Action/Navigation", id: "d9aaad170f4f9e35c8485d219d33a66a")]
    public partial class CalculateDirectionAction : Action {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<Vector2> NextPosition;
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected override Status OnStart() {
            if (!Self.Value) {
                Logging.LogError("Self is null");
                return Status.Failure;
            }

            Direction.Value = (NextPosition.Value - (Vector2)Self.Value.transform.position).normalized;
            return Status.Success;
        }
    }
}