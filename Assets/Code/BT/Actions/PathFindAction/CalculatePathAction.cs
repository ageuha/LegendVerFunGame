using Member.YDW.Script.PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CalculatePath", story: "[PathAgent] Calculate [Path] to [Destination]",
    category: "Action/Navigation", id: "042dd6f85c4e068b8c020819adf8a8f0")]
public partial class CalculatePathAction : Action {
    [SerializeReference] public BlackboardVariable<PathAgent> PathAgent;
    [SerializeReference] public BlackboardVariable<List<Vector3>> Path;
    [SerializeReference] public BlackboardVariable<Vector3> Destination;
    private static Vector3[] _path = new Vector3[1000];

    protected override Status OnStart() {
        if (!PathAgent.Value) {
            Logging.LogError("PathAgent is null");
            return Status.Failure;
        }

        Vector3Int startCell = Vector3Int.FloorToInt(PathAgent.Value.transform.position);
        Logging.Log($"Start Cell: {startCell}");
        Vector3Int endCell = Vector3Int.FloorToInt(Destination.Value);
        var pathCount = PathAgent.Value.GetPath(startCell, endCell, _path);
        if (pathCount <= 0) {
            return Status.Failure;
        }

        Path.Value = _path[..pathCount].ToList();
        return Status.Success;
    }
}