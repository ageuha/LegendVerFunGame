using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;
using YTH.Code.Item;

namespace Member.KJW.Code.Data
{
    [CreateAssetMenu(fileName = "PlaceableItemData", menuName = "SO/Item/PlaceableItemData", order = 0)]
    public class PlaceableItemData : ItemDataSO
    {
        [field: SerializeField] public BuildingDataSO BuildingData { get; private set; }
    }
}