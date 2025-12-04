using UnityEngine;
using UnityEngine.UI;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class TempInventorySlot : MonoBehaviour
    {
        private Image m_icon;
        private ItemDataSO m_itemDataSO;

        private void Awake()
        {
            m_icon ??= GetComponent<Image>();
        }
        
        private void SetItemData(ItemDataSO itemDataSO)
        {
            this.m_itemDataSO = itemDataSO;
            m_icon.sprite = m_itemDataSO.Icon;
        }
    }
}
