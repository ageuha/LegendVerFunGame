using System;

namespace Code.Core.GlobalInterfaces {
    public interface ISubscribable<out T> {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }
}