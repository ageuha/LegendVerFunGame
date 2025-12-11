using Code.EntityScripts;
using Member.JJW.Code.ResourceObject;
using Member.JJW.Code.SO;

namespace Member.JJW.Code.Interface
{
    public interface IHarvestable
    {
        public HealthSystem CurrentHp {get; set; }
        public void Harvest(ItemInfo itemInfo);
        public void Initialize(ResourceSO resourceSO);
    }
}