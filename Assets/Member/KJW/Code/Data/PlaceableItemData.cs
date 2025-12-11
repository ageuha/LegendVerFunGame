using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.KJW.Code.Data
{
    [CreateAssetMenu(fileName = "PlaceableItemData", menuName = "SO/Item/PlaceableItemData", order = 0)]
    public class PlaceableItemData : ItemDataSO
    {
        [field: SerializeField] public BuildingDataSO BuildingData { get; private set; }
    }
}