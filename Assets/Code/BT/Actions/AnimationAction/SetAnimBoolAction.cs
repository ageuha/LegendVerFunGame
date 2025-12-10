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
    [NodeDescription(name: "SetAnimBool", story: "Set [Animator] [Hash] [Value]", category: "Action/Animation",
        id: "d213bc45bdd64a225e4f45247c3665c0")]
    public partial class SetAnimBoolAction : Action {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> Hash;
        [SerializeReference] public BlackboardVariable<bool> Value;

        protected override Status OnStart() {
            if (!Animator.Value) {
                Logging.LogError("SetAnimBoolAction의 AnimatorCompo가 null임,,");
                return Status.Failure;
            }

            if (!Hash.Value) {
                Logging.LogError("SetAnimBoolAction의 Hash가 null임,,");
                return Status.Failure;
            }

            Animator.Value.SetValue(Hash.Value, Value.Value);
            return Status.Success;
        }
    }
}