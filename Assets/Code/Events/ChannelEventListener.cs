using Code.Core.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Events {
    public class ChannelEventListener<T> : MonoBehaviour {
        [SerializeField] private SerializeHelper<EventChannel<T>> eventChannel;
        [field: SerializeField] public UnityEvent<T> OnEvent { get; private set; }

        private void Awake() {
            eventChannel.Value.OnEvent += HandleEvent;
        }

        private void OnDestroy() {
            eventChannel.Value.OnEvent -= HandleEvent;
        }

        private void HandleEvent(T value) => OnEvent?.Invoke(value);
    }
}