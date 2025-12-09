using System;
using Unity.Behavior;
using UnityEngine;

namespace Code.EntityScripts.BaseClass {
    [RequireComponent(typeof(BehaviorGraphAgent))]
    public abstract class GraphEntity : Entity {
        [field: SerializeField] public BehaviorGraphAgent GraphAgent { get; private set; }

        protected virtual void Reset() {
            GraphAgent ??= GetComponent<BehaviorGraphAgent>();
        }
    }
}