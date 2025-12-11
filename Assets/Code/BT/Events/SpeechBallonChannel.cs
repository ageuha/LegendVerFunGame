using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace Code.BT.Events {
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/SpeechBallonChannel")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "SpeechBallonChannel", message: "[Sprite] and [Duration]", category: "Events", id: "ede0b955515551e98236cbcbf7d94527")]
    public sealed partial class SpeechBallonChannel : EventChannel<Sprite, float> { }
}

