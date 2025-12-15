using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace Code.BT.Actions.PathFindAction {
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "MoveAlongPath", story: "Return false when [Path] is End [PathIndex]", category: "Conditions", id: "34fded513c5a9c660a7bd762da91424a")]
    public partial class MoveAlongPathCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<List<Vector3>> Path;
        [SerializeReference] public BlackboardVariable<int> PathIndex;

        public override bool IsTrue()
        {
            if (Path.Value == null)
            {
                Debug.LogError("Path is null");
                return false;
            }

            return ++PathIndex.Value < Path.Value.Count;
        }
    }
}
