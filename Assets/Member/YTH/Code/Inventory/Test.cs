using UnityEngine;
using YTH.Code.Inventory;
using YTH.Code.Item;

namespace YTH.Code.Test
{    
    public class Test : MonoBehaviour
    {
        [SerializeField] private ItemDataSO itemDataSO;
        [SerializeField] private int amount;
        [SerializeField] private InventoryData inventoryData;

        public void Testing()
        {
            inventoryData.AddItem(new InventoryItem(itemDataSO, amount));
        }
    }
}
