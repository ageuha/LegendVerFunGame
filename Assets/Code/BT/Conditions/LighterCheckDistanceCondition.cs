using System;
using Code.Core.Utility;
using Unity.Behavior;
using UnityEngine;

namespace Code.BT.Conditions {
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "LighterCheckDistance", story: "distance between [Transform] and [Target] [Operator] [Threshold]", category: "Conditions", id: "a4e2935d6444ad6eee4d28c460c96a47")]
    public partial class LighterCheckDistanceCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [Comparison(comparisonType: ComparisonType.All)]
        [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
        [SerializeReference] public BlackboardVariable<float> Threshold;

        public override bool IsTrue()
        {
            if (!Transform.Value || !Target.Value)
            {
                Logging.LogError("LighterCheckDistanceCondition : 뭔가가 null 임");
                return false;
            }

            float distance = (Target.Value.position - Transform.Value.position).sqrMagnitude;
            return ConditionUtils.Evaluate(distance, Operator, Threshold.Value * Threshold.Value);
        }
    }
}
