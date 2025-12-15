using System;
using System.Collections.Generic;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.PathFindAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set NextPosition", story: "Set [NextPosition] [Path] [PathIndex]",
        category: "Action/Navigation", id: "ab0af974b6de9c44ebc7caf76454dfd9")]
    public partial class SetNextPositionAction : Action {
        [SerializeReference] public BlackboardVariable<Vector2> NextPosition;
        [SerializeReference] public BlackboardVariable<List<Vector3>> Path;
        [SerializeReference] public BlackboardVariable<int> PathIndex;

        protected override Status OnStart() {
            if (Path.Value == null) {
                Logging.LogError("Path is null");
                return Status.Failure;
            }

            if (PathIndex.Value < 0 || PathIndex.Value >= Path.Value.Count) {
                Logging.LogError("PathIndex is out of range");
                return Status.Failure;
            }

            NextPosition.Value = Path.Value[PathIndex.Value];
            return Status.Success;
        }
    }
}