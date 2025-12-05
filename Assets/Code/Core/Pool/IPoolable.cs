using UnityEngine;

namespace Code.Core.Pool {
    public interface IPoolable {
        GameObject GameObject { get; }
        int InitialCapacity { get; }

        public void OnPopFromPool();

        public void OnReturnToPool();
    }
}