using UnityEngine;

namespace Code.Core.Pool {
    public static class PoolFactoryContainer<T> where T : MonoBehaviour, IPoolable {
        private static TypeSafePoolFactory<T> _factory;

        public static void InitializeFactory(T prefab, int initialCapacity, Transform parent) {
            Debug.Assert(_factory == null, "팩토리가 이미 존재하는데 또 초기화 됨");
            _factory = new TypeSafePoolFactory<T>(prefab, initialCapacity, parent);
        }
        
        public static TypeSafePoolFactory<T> Factory => _factory;
    }
}