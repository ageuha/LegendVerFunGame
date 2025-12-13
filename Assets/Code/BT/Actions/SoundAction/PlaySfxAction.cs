using System;
using Code.Core.Utility;
using Code.SoundSystem;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Code.BT.Actions.SoundAction {
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "PlaySFX", story: "[SoundPlayer] Play [Sound]", category: "Action", id: "946b6c0df274efa8e11860d01167c7e6")]
    public partial class PlaySfxAction : Action
    {
        [SerializeReference] public BlackboardVariable<SFXPlayer> SoundPlayer;
        [SerializeReference] public BlackboardVariable<AudioClip> Sound;

        protected override Status OnStart()
        {
            if (!SoundPlayer.Value) {
                Logging.LogError("SoundPlayer가 없네?");
                return Status.Failure;
            }

            if (!Sound.Value) {
                Logging.LogError("Sound가 없네?");
                return Status.Failure;
            }
            
            SoundPlayer.Value.Play(Sound.Value);
            return Status.Success;
        }
    }
}

