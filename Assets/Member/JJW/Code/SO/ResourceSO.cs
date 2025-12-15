using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.JJW.Code.SO
{
    [CreateAssetMenu(fileName = "ResourceSO", menuName = "JJW/ResourceSO", order = 0)]
    public class ResourceSO : ScriptableObject
    {
        [field: SerializeField] public float MaxHp { get; private set; }

        public Sprite ResourceImage;
        public ItemDataSO ItemDataSO;
        public int SpawnItemAmount = 1;
        public float ItemSpawnRadius = 1f;
        public Vector2Int DetectionRangeSize;
    }
}