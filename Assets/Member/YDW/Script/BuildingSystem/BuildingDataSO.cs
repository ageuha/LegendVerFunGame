using System;
using System.Collections.Generic;
using Code.Core.Utility;
using Code.GridSystem.Objects;
using Member.YDW.Script.NewBuildingSystem.Buildings;
using UnityEngine;
using YTH.Code.Item;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingSystem/BuildingData", order = 0)]
    public class BuildingDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public float BuildTime { get; private set; }
        [field: SerializeField] public GridObject Building { get; private set; }
        [field: SerializeField] public Type RealType {get; private set;}
        [field: SerializeField] public Vector2Int BuildingSize { get; private set; }
        [field: SerializeField] public int InitialCapacity { get; private set; }

        private void OnValidate()
        {
            if (Building != null)
            {
                RealType = Building.GetType();
                Logging.Log($"RealType is {RealType}");
            }
        }
    }
}