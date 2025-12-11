using System;
using Code.Core.Utility;
using Code.EntityScripts.Components;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.AnimationAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetFlip", story: "[Renderer] Flip by [direction]", category: "Action/Animation",
        id: "d94ccf5282989d7fc4bb42f19cf64479")]
    public partial class SetFlipAction : Action {
        [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
        [SerializeReference] public BlackboardVariable<Vector2> Direction;

        protected override Status OnStart() {
            if (!Renderer.Value) {
                Logging.LogError("Renderer null이다.");
                return Status.Failure;
            }

            Renderer.Value.SetFlip(Direction.Value.x);
            return Status.Success;
        }
    }
}