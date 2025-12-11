using Member.YTH.Code.Item;

namespace Member.YDW.Script.CookingSystem
{
    public interface ICookable
    {
        public ItemDataSO  CookedItemData { get; }
        public float CookTime { get; }
        public bool IsCooked { get; }
    }
}