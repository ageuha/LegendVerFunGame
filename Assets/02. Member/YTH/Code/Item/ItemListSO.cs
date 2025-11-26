using System.Collections.Generic;
using UnityEngine;

namespace YTH.Item
{    
    [CreateAssetMenu(fileName = "ItemListSO", menuName = "SO/List/Item")]
    public class ItemListSO : ScriptableObject
    {
        [field:SerializeField] public List<ItemSO> ItemSOList { get; private set; }
    }
}
