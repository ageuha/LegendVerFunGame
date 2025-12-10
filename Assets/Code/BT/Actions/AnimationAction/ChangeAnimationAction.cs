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
    [NodeDescription(name: "ChangeAnimation", story: "[Animator] Change [Prev] to [Current]",
        category: "Action/Animation", id: "1aae75ed8318fc17942f94bf92d2e949")]
    public partial class ChangeAnimationAction : Action {
        [SerializeReference] public BlackboardVariable<AnimatorCompo> Animator;
        [SerializeReference] public BlackboardVariable<HashSO> Prev;
        [SerializeReference] public BlackboardVariable<HashSO> Current;

        protected override Status OnStart() {
            if (Prev.Value) {
                if (Prev.Value == Current.Value) {
                    return Status.Success;
                }
                Animator.Value.SetValue(Prev.Value, false);
            }

            if (!Current.Value) {
                Logging.LogError("Current가 null임 ㅠㅠ");
                return Status.Failure;
            }
            
            Animator.Value.SetValue(Current.Value, true);

            return Status.Success;
        }
    }
}