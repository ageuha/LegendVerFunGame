using Code.Core.Pool;
using Code.EntityScripts.BaseClass;

namespace Code.EntityScripts.ConcreteClass {
    public class Chicken : FriendlyEntityBase, IPoolable {
        public override void PushToPool() {
            PoolManager.Instance.Factory<Chicken>().Push(this);
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