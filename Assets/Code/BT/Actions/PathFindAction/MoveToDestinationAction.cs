using System;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveToDestination", story: "[PathMovement] Move to [Destination]", category: "Move",
    id: "c7cce536a92bca402074353a2d61977e")]
public partial class MoveToDestinationAction : Action {
    [SerializeReference] public BlackboardVariable<PathMovement> PathMovement;
    [SerializeReference] public BlackboardVariable<Vector3> Destination;

    protected override Status OnStart() {
        if (!PathMovement.Value) {
            Logging.LogError("PathMovement is null");
            return Status.Failure;
        }

        PathMovement.Value.SetDestination(Destination.Value);
        if (PathMovement.Value.IsPathFailed)
            return Status.Failure;
        return Status.Running;
    }

    protected override Status OnUpdate() {
        if (PathMovement.Value.IsArrived)
            return Status.Success;
        return Status.Running;
    }
}