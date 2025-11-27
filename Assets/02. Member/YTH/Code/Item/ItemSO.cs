using UnityEngine;

namespace _02._Member.YTH.Code.Item
{    
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        [field:SerializeField] public int ItemID { get; private set; }
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public Sprite ItemIcon { get; private set; }
        [field:SerializeField] public int MaxStackAmount { get; private set; }


        private void OnValidate()
        {
            if (MaxStackAmount < 1)
            {
                MaxStackAmount = 1;
            }

            if (ItemID < 1)
            {
                ItemID = 1;
            }
        }
    }
}
