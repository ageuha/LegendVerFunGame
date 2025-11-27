using System;
using UnityEngine;

namespace Code.Events {
    public abstract class EventChannel<T> : ScriptableObject {
        public event Action<T> OnEvent;
        public void Raise(T item) => OnEvent?.Invoke(item);
    }
}