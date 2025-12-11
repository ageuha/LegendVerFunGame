using System;
using Code.AnimatorSystem;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.AnimationAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetAnimSpeed", story: "Set [Animator] Speed [Value]", category: "Action/Animation",
        id: "5c67f3b1cc2c5bb44c11d3710889d979")]
    public partial class SetAnimSpeedAction : Action {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<float> Value;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("Animator가 null이야ㅑㅑㅑㅑㅑ");
                return Status.Failure;
            }

            Animator.Value.AnimationSpeed = Value.Value;
            return Status.Success;
        }
    }
}