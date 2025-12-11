using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.MoverAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "SetRandomDirection", story: "Set [Direction] Random", category: "Move", id: "a6f82de8015ab68a6b74be1b0603b38c")]
    public partial class SetRandomDirectionAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;

        protected override Status OnStart()
        {
            Direction.Value = UnityEngine.Random.insideUnitCircle;
            Direction.Value.Normalize();
            return Status.Success;
        }
    }
}

