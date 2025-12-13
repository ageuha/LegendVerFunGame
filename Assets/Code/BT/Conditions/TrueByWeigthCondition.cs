using System;
using Unity.Behavior;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TrueByWeight", story: "True in [Value]", category: "Conditions", id: "5039bff72aed20c99ab7f2975e0c496d")]
public partial class TrueByWeigthCondition : Condition // 오타 고치면 BT가 터짐;;
{
    [SerializeReference] public BlackboardVariable<float> Value;

    public override bool IsTrue()
    {
        if(Random.value < Value.Value) return true;
        return false;
    }
}
