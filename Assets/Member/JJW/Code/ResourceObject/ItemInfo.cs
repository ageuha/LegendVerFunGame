using System;

namespace Member.JJW.Code.ResourceObject
{
    public enum ItemType
    {
        Pick,
        Sword,
        Axe,
        Default
    }

    public enum ItemIngredient
    {
        Wood = 2,
        Rock = 3,
        Copper = 5,
        Iron = 10,
        Gold =  17,
        Diamond = 30,
        Default = 0
    }

    [Serializable]
    public struct ItemInfo
    {
        public ItemType ItemType;
        public ItemIngredient Ingredient;
    }
}