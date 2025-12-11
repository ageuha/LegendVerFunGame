using Member.YTH.Code.Item;
using UnityEngine;
using YTH.Code.Inventory;

namespace YTH.Code.Test
{    
    public class TestButton : MonoBehaviour
    {
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private ItemDataSO item;

        public void Test()
        {
            inventoryAddEventChannel.Raise(item);
        }
    }
}
