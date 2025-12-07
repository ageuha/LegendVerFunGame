using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class TempInventorySlot : MonoBehaviour
    {
        [SerializeField] private Image m_icon;
        [SerializeField] private TextMeshProUGUI m_countText;
        private ItemDataSO m_itemDataSO;
        private int m_count;

        private void Awake()
        {
            UpdateUI();
        }
        
        public void SetItemData(ItemDataSO itemDataSO, int count)
        {
            this.m_itemDataSO = itemDataSO;
            this.m_count = count;
            UpdateUI();
        }

        public void Reset()
        {
            this.m_itemDataSO = null;
            this.m_count = 0;
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (m_itemDataSO == null)
            {
                m_icon.sprite = null;
                m_countText.text = string.Empty;
                return;
            }

            if (m_count <= 0 || m_itemDataSO.Icon == null)
            {
                m_icon.sprite = null;
                m_countText.text = string.Empty;
                return;
            } 

            m_icon.sprite = m_itemDataSO.Icon;
            m_countText.text = $"{m_count}개";

        }
    }
}
