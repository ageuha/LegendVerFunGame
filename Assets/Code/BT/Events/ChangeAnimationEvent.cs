using System;
using Code.Core.GlobalSO;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace Code.BT.Events {
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/ChangeAnimationEvent")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "ChangeAnimationEvent", message: "Change animation to [Hash]", category: "Events", id: "fc65b12695f211390376b21384005698")]
    public sealed partial class ChangeAnimationEvent : EventChannel<HashSO> { }
}

