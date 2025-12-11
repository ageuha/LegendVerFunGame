using System;
using Code.Core.Utility;
using Code.UI;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.AnimationAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Speech", story: "[SpeechBalloon] Show [Sprite] for [Duration]",
        category: "Action/Animation", id: "25ed30bff1f858da9a9d60e8126318f0")]
    public partial class SpeechAction : Action {
        [SerializeReference] public BlackboardVariable<SpeechBalloon> SpeechBalloon;
        [SerializeReference] public BlackboardVariable<Sprite> Sprite;
        [SerializeReference] public BlackboardVariable<float> Duration;
        private float _endTime;

        protected override Status OnStart() {
            if (!SpeechBalloon.Value) {
                Logging.LogError("말풍선 null!!!");
                return Status.Failure;
            }

            _endTime = Time.time + Duration.Value;
            SpeechBalloon.Value.EnableFor(Sprite.Value);
            return Status.Running;
        }

        protected override Status OnUpdate() {
            if (Time.time <= _endTime) return Status.Running;
            SpeechBalloon.Value.Disable();
            return Status.Success;
        }
    }
}