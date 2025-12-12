using Code.EntityScripts;
using Member.JJW.Code.ResourceObject;
using Member.JJW.Code.SO;
using Member.YTH.Code.Item;

namespace Member.JJW.Code.Interface
{
    public interface IHarvestable
    {
        public HealthSystem CurrentHp {get; set; }
        public void Harvest(ItemDataSO itemInfo);
        public void Initialize(ResourceSO resourceSO);
    }
}