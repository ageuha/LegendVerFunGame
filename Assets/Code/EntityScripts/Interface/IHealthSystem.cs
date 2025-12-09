using System;
using Code.Core.Utility;

namespace Code.EntityScripts.Interface {
    public interface IHealthSystem {
        IReadOnlyNotifyValue<float> Hp { get; }
        event Action OnDead;
    }
}