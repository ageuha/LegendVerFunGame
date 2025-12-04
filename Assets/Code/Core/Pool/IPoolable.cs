using UnityEngine;

namespace Code.Core.Pool {
    public interface IPoolable {
        GameObject GameObject { get; }

        public void OnPopFromPool();

        public void OnReturnToPool();
    }
}