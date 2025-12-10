using System;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StopImmediately", story: "[Mover] StopImmediately", category: "Move", id: "16d468e6dfe5bf7aabe24f85184231bd")]
    public partial class StopImmediatelyAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;

        protected override Status OnStart()
        {
            if (!Mover.Value) {
                Logging.LogError("StopImmediatelyAction에 Mover 참조 까먹으셨어요");
                return Status.Failure;
            }
            Mover.Value.StopImmediately();
            return Status.Success;
        }
    }
}

