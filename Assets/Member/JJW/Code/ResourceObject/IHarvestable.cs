using Code.EntityScripts;
using Member.JJW.Code.SO;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.JJW.Code.ResourceObject
{
    public interface IHarvestable
    {
        [field: SerializeField] public HealthSystem CurrentHp { get; set; }
        public void Harvest(ItemDataSO itemInfo);
        public void Initialize(ResourceSO resourceSO);
    }
}