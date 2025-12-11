using Member.JJW.Code.ResourceObject;
using UnityEngine;
using YTH.Code.Item;

namespace Member.JJW.Code.SO
{
    [CreateAssetMenu(fileName = "ResourceSO", menuName = "JJW/ResourceSO", order = 0)]
    public class ResourceSO : ScriptableObject
    {
        [field:SerializeField] public float MaxHp {get; private set; }

        public ItemType correctType;
        public Sprite ResourceImage;
        public GameObject ItemPrefab;
        public ItemDataSO ItemDataSO;
        public int SpawnItemAmount =1;
        public float ItemSpawnRadius = 1f;
    }
}