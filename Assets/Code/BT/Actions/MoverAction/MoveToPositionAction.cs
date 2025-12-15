using Code.EntityScripts.Components;
using System;
using Code.Core.Utility;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToPosition", story: "[Mover] MoveTo [NextPosition]", category: "Action/Navigation",
    id: "67f26c4da247b5110756618cebf14573")]
public partial class MoveToPositionAction : Action {
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Vector2> NextPosition;
    private Vector2 _previousPosition;
    private Vector2 CurrentPosition => Mover.Value ? Mover.Value.transform.position : Vector2.zero;

    protected override Status OnStart() {
        if (!Mover.Value) {
            Logging.LogError("Mover is null");
            return Status.Failure;
        }

        var direction = (NextPosition.Value - (Vector2)Mover.Value.transform.position).normalized;
        Mover.Value.SetMovementInput(direction);
        _previousPosition = CurrentPosition;
        return Status.Running;
    }

    protected override Status OnUpdate() {
        var currentDirection = (NextPosition.Value - CurrentPosition).normalized;
        var beforeDirection = (NextPosition.Value - _previousPosition).normalized;
        if (Vector2.Dot(beforeDirection, currentDirection) <= 0
            || Vector2.Distance(NextPosition.Value, CurrentPosition) < 0.01f) {
            return Status.Success;
        }

        return Status.Running;
    }
}