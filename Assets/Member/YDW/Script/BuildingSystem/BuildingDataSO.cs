using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "BuildingSystem/BuildingData", order = 0)]
    public class BuildingDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public float BuildTime { get; private set; }
        [field: SerializeField] public GameObject BuildingPrefab { get; private set; }
        [field: SerializeField] public GameObject BuildingWaitPrefab { get; private set; }
        [field: SerializeField] public Vector2Int BuildingSize { get; private set; }
        
        
    }
}