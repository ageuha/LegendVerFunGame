using Code.Core.Pool;
using Code.EntityScripts.BaseClass;

namespace Code.EntityScripts.ConcreteClass {
    public class Cow : BadEntityBase, IPoolable {
        public override void PushToPool() {
            PoolManager.Instance.Factory<Cow>().Push(this);
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