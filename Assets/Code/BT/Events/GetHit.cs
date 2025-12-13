using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;

namespace Code.BT.Events {
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/GetHit")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "GetHit", message: "[Self] Hitten by [BadGuy]", category: "Events", id: "00c5c707a0329547d403a164a8ae7979")]
    public sealed partial class GetHit : EventChannel<GameObject, GameObject> { }
}

