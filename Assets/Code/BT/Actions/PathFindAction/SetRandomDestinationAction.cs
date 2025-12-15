using System;
using Code.Core.Utility;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetRandomDestination", story: "[Self] set Random [Destination] in [Radius]",
    category: "Action/Navigation", id: "c303ee236fa8da2fafafba45aeb42eaf")]
public partial class SetRandomDestinationAction : Action {
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> Destination;
    [SerializeReference] public BlackboardVariable<float> Radius;

    protected override Status OnStart() {
        if (!Self.Value) {
            Debug.LogError("Self is null");
            return Status.Failure;
        }

        Vector3 randomDir = Random.insideUnitCircle.normalized * Radius.Value;
        var hit = Physics2D.Raycast(Self.Value.transform.position, randomDir, Radius.Value, 1 << 13);
        if (hit.collider) {
            Destination.Value = hit.point;
        }
        else {
            Destination.Value = Self.Value.transform.position + randomDir;
        }
        Debug.DrawLine(Self.Value.transform.position, Destination.Value, Color.green, 2f);
        
        Logging.Log(Destination.Value);

        return Status.Success;
    }
}