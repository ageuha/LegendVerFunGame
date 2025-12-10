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
    [NodeDescription(name: "SetAnimTrigger", story: "Set [Animator] [Hash] Trigger", category: "Action/Animation",
        id: "203fb270bff14309dc68e5086edb6df3")]
    public partial class SetAnimTriggerAction : Action {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> Hash;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("SetAnimIntAction의 AnimatorCompo가 null임,,");
                return Status.Failure;
            }

            if (!Hash.Value) {
                Logging.LogError("SetAnimIntAction의 Hash가 null임,,");
                return Status.Failure;
            }

            Animator.Value.SetValue(Hash.Value);
            return Status.Success;
        }
    }
}