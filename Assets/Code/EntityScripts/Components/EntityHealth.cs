using Code.EntityScripts.BaseClass;
using Code.EntityScripts.Interface;

namespace Code.EntityScripts.Components {
    public class EntityHealth : HealthSystem, IEntityModule {
        public void Initialize(Entity owner) {
            Initialize(owner.Data.MaxHp);
        }
    }
}