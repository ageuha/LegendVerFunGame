using System;
using Code.AnimatorSystem;
using Code.Core.GlobalSO;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.AnimationAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetAnimVector", story: "Set [Animator] [HashX] [HashY] [Vector2]", category: "Action/Animation", id: "5af8b7a19818f79ee4ae7553087295de")]
    public partial class SetAnimVectorAction : Action
    {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> HashX;
        [SerializeReference] public BlackboardVariable<HashSO> HashY;
        [SerializeReference] public BlackboardVariable<Vector2> Vector2;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("SetAnimIntAction의 AnimatorCompo가 null임,,");
                return Status.Failure;
            }

            if (!HashX.Value) {
                Logging.LogError("SetAnimIntAction의 HashX가 null임,,");
                return Status.Failure;
            }
            
            if (!HashY.Value) {
                Logging.LogError("SetAnimIntAction의 HashY가 null임,,");
                return Status.Failure;
            }

            Animator.Value.SetValue(HashX.Value, Vector2.Value.x);
            Animator.Value.SetValue(HashY.Value, Vector2.Value.y);
            return Status.Success;
        }
    }
}

