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
    [NodeDescription(name: "SetAnimInt", story: "Set [Animator] [Hash] [Value]", category: "Action/Animation",
        id: "c37d2c6371b0c9124b8a38d6d4fd578f")]
    public partial class SetAnimIntAction : Action {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> Hash;
        [SerializeReference] public BlackboardVariable<int> Value;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("SetAnimIntAction의 AnimatorCompo가 null임,,");
                return Status.Failure;
            }

            if (!Hash.Value) {
                Logging.LogError("SetAnimIntAction의 Hash가 null임,,");
                return Status.Failure;
            }

            Animator.Value.SetValue(Hash.Value, Value.Value);
            return Status.Success;
        }
    }
}