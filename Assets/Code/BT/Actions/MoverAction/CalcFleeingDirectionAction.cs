using System;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "CalcFleeingDirection", story: "Set [Direction] Fleeing from [BadGuy] [Self]",
        category: "Move", id: "f9d05db286f9f215852f093720ddfaf0")]
    public partial class CalcFleeingDirectionAction : Action {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<GameObject> BadGuy;
        [SerializeReference] public BlackboardVariable<GameObject> Self;

        protected override Status OnStart() {
            if (!BadGuy.Value) {
                Logging.Log("도망갈 대상이 없음");
                return Status.Success; // 대상이 없어도 일단 넘어가
            }

            if (!Self.Value) {
                Logging.LogError("자신이 없음,,");
                return Status.Failure;
            }

            Direction.Value = (Self.Value.transform.position - BadGuy.Value.transform.position).normalized;
            return Status.Success;
        }
    }
}