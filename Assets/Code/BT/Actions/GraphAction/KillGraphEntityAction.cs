using System;
using Code.Core.Utility;
using Code.EntityScripts.BaseClass;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.GraphAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Kill GraphEntity", story: "Kill [GraphEntity]", category: "Action", id: "835b8b00ed992c5888c7247fa455b953")]
    public partial class KillGraphEntityAction : Action
    {
        [SerializeReference] public BlackboardVariable<GraphEntity> GraphEntity;

        protected override Status OnStart()
        {
            if (!GraphEntity.Value) {
                Logging.LogError("GraphEntity가 null이야 ^^");
                return Status.Failure;
            }
            
            GraphEntity.Value.EndGraph();
            return Status.Success;
        }
    }
}

