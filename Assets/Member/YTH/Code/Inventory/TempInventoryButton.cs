using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class TempInventoryButton : MonoBehaviour
    {
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private ItemDataSO itemDataSO;

        public void Test()
        {
            inventoryAddEventChannel.Raise(itemDataSO);
        }
    }
}
