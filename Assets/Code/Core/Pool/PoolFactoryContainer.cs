using UnityEngine;

namespace Code.Core.Pool {
    public static class PoolFactoryContainer<T> where T : PoolableObject {
        private static PoolFactory<T> _factory;

        public static void InitializeFactory(T prefab, int initialCapacity) {
            Debug.Assert(_factory == null, "팩토리가 이미 존재하는데 또 초기화 됨");
            _factory = new PoolFactory<T>(prefab, initialCapacity);
        }
        
        public static PoolFactory<T> Factory => _factory;
    }
}