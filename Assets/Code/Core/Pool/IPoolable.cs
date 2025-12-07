namespace Code.Core.Pool {
    public interface IPoolable {
        int InitialCapacity { get; }

        public void OnPopFromPool();

        public void OnReturnToPool();
    }
}