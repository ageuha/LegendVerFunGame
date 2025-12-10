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
    [NodeDescription(name: "SetAnimFloat", story: "Set [Animator] [Hash] [Value]", category: "Action/Animation", id: "10fb165eec56f531e625f9ac1ad5f852")]
    public partial class SetAnimFloatAction : Action
    {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> Hash;
        [SerializeReference] public BlackboardVariable<float> Value;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("SetAnimFloatAction의 AnimatorCompo가 null임,,");
                return Status.Failure;
            }

            if (!Hash.Value) {
                Logging.LogError("SetAnimFloatAction의 Hash가 null임,,");
                return Status.Failure;
            }

            Animator.Value.SetValue(Hash.Value, Value.Value);
            return Status.Success;
        }
    }
}

