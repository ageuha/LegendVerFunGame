using Code.Core.Pool;
using Code.EntityScripts.BaseClass;

namespace Code.EntityScripts.ConcreteClass {
    public class Turtle : FriendlyEntityBase, IPoolable {
        public override void PushToPool() {
            PoolManager.Instance.Factory<Turtle>().Push(this);
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