using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;

namespace Member.YTH.Code.Item
{    
    [CreateAssetMenu(fileName = "ItemDataListSO", menuName = "SO/List/ItemData")]
    public class ItemDataListSO : ScriptableObject
    {
        [field:SerializeField] public List<ItemDataSO> ItemDataList { get; private set; }

        private Dictionary<int, ItemDataSO> m_ItemDataDictionary;

        public void Initialize() 
        {
            m_ItemDataDictionary ??= ItemDataList.ToDictionary(item => item.ItemID);

            foreach (var item in m_ItemDataDictionary)
            {
                Logging.Log($"{item.Key} {item.Value}");
            }
        }

        public ItemDataSO this[int hash]
        {
            get
            {
                if(m_ItemDataDictionary.TryGetValue(hash, out var item))
                {
                    return item;
                }

                return null;
            }
            
        }
    }
}

