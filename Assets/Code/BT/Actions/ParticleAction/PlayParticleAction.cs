using System;
using Code.Core.Utility;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.ParticleAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PlayParticle", story: "Play [Particle]", category: "Action/Particle", id: "d5c50e0eb39e0c9b0a1a9186e1b43689")]
    public partial class PlayParticleAction : Action
    {
        [SerializeReference] public BlackboardVariable<ParticleSystem> Particle;

        protected override Status OnStart() {
            if (!Particle.Value) {
                Logging.LogError("EmitFeatherAction에 파티클이 Null임");
                return Status.Failure;
            }
            
            Particle.Value.Play();
            return Status.Success;
        }
    }
}

