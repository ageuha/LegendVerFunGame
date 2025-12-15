using Code.Core.Pool;
using Code.EntityScripts.BaseClass;

namespace Code.EntityScripts.ConcreteClass {
    public class Kiwi : FriendlyEntityBase, IPoolable {
        public override void PushToPool() {
            PoolManager.Instance.Factory<Kiwi>().Push(this);
        }

        public int InitialCapacity => 10;

        public void OnPopFromPool() {
            GraphAgent.Start();
        }

        public void OnReturnToPool() {
            EndGraph();
            ResetEntity();
        }
    }
}