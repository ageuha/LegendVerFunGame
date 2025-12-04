using Code.Core.Pool;
using UnityEngine;

namespace Code.Tests {
    public class TestPoolable : MonoBehaviour, IPoolable {
        public GameObject GameObject => gameObject;

        public void OnPopFromPool() {
        }

        public void OnReturnToPool() {
        }
    }
}