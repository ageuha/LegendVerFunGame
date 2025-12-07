using Code.EntityScripts;
using UnityEngine;
using YTH.Code.Stat;

namespace YTH.Code.Item
{
    [CreateAssetMenu(fileName = "FoodItemDataSO", menuName = "SO/Item/Food")]
    public abstract class FoodItemDataSO : ItemDataSO
    {
        [Header("Food")]
        [field: SerializeField] public bool Edible { get; private set; } //먹을 수 있냐 없냐.
        [field: SerializeField] public float HealAmount { get; private set; }
        [field: SerializeField] public Sprite CookingImage { get; private set; }
        
        

        public virtual void Eat(HealthSystem healthSystem)
        {
            if(!Edible) return;
            healthSystem.ApplyHeal(HealAmount);
        }

        public virtual void Cooked()
        {
            Edible = true;
            
        }
    }
}
