using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YTH.Code.Inventory
{    
    public class TempInventoryUI : MonoBehaviour
    {
        private List<TempInventorySlot> m_tempInventorySlots;

        private void Awake()
        {
            m_tempInventorySlots = GetComponentsInChildren<TempInventorySlot>().ToList();
        }
    }
}
