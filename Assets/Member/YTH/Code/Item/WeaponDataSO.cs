using System;
using System.Collections.Generic;
using Member.KJW.Code.Data;
using UnityEngine;
using YTH.Code.Stat;

namespace Member.YTH.Code.Item
{    
    [CreateAssetMenu(fileName = "EquipmentDataSO", menuName = "SO/Item/Equipment")]
    public class WeaponDataSO : ItemDataSO
    {
        [field:SerializeField] public EquipmentType EquipmentType { get; private set; }
        [field:SerializeField] public AttackData AttackData { get; private set; }
        [field:SerializeField] public List<Stat> Stats { get; private set; }
        [field:SerializeField] public Vector2 HitBoxOffset { get; private set; }
        [field:SerializeField] public Vector2 HitBoxSize { get; private set; }
        [field:SerializeField] public float ThrowLifeTime { get; private set; }
    }
    
    [Serializable]
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
