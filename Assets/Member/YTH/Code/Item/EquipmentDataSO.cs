using System.Collections.Generic;
using UnityEngine;
using YTH.Code.Stat;

namespace YTH.Code.Item
{    
    [CreateAssetMenu(fileName = "EquipmentDataSO", menuName = "SO/Item/Equipment")]
    public class EquipmentDataSO : ItemDataSO
    {
        [field:SerializeField] public EquipmentType EquipmentType { get; private set; }
        [field:SerializeField] public List<Stat> Stats { get; private set; }
    }
    
    
    public class Stat
    {
        public StatBaseSO StatData;
        public int Amount;

        public Stat(StatBaseSO statData, int amount)
        {
            this.StatData = statData;
            this.Amount = amount;
        }

        public override string ToString()
        {
            return $"{StatData} +{Amount}";
        }
    }

    public enum EquipmentType
    {
        Sword,
        Axe,
        Pickaxe,
        Necklace,
        Ring
    }
}
