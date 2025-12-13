using Code.Core.Utility;
using Member.YTH.Code.Item;
using UnityEngine;

namespace YTH.Code.Item
{    
    public class GetItemData : MonoSingleton<GetItemData>
    {
        [field:SerializeField] public ItemDataListSO ItemDataListSO { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ItemDataListSO.Initialize();
        }
    }
}
