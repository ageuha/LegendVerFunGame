using System.Collections.Generic;
using YTH.Item;
using UnityEngine;

namespace _02._Member.YTH.Code.Item
{    
    [CreateAssetMenu(fileName = "ItemListSO", menuName = "SO/List/Item")]
    public class ItemListSO : ScriptableObject
    {
        [field:SerializeField] public List<ItemSO> ItemSOList { get; private set; }
    }
}
