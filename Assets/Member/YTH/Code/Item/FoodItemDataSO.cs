using Code.EntityScripts;
using Member.YDW.Script.CookingSystem;
using UnityEngine;

namespace YTH.Code.Item
{
    [CreateAssetMenu(fileName = "FoodItemDataSO", menuName = "SO/Item/Food")]
    public abstract class FoodItemDataSO : ItemDataSO , ICookable
    {
        [Header("Food")]
        [field: SerializeField] public bool Edible { get; private set; } //먹을 수 있냐 없냐.
        [field: SerializeField] public float HealAmount { get; private set; }
        [field: SerializeField] public ItemDataSO CookedItemData { get; private set; }
        [field: SerializeField] public float CookTime { get; private set; }
        [field: SerializeField] public bool IsCooked { get; private set; }


        public virtual void Eat(HealthSystem healthSystem)
        {
            if(!Edible) return;
            healthSystem.ApplyHeal(HealAmount);
        }
        
        
    }
}
