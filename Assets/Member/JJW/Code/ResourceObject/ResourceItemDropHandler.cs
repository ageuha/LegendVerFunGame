using Code.Core.Pool;
using Member.YTH.Code.Item;
using UnityEngine;
using YTH.Code.Item;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.ResourceObject
{
    public class ResourceItemDropHandler : MonoBehaviour
    {
        [SerializeField] private Resource resource;

        private void Awake()
        {
            resource = gameObject.GetComponent<Resource>();
        }

        private void OnEnable()
        {
            if (resource == null) return;
            resource.CurrentHp.OnDead += SpawnItem;
        }

        private void SpawnItem()
        {
            if(resource == null) return;
            for (int i = 0; i < resource.ResourceSO.SpawnItemAmount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                ItemObject item = PoolManager.Instance.Factory<ItemObject>().Pop();
                item.transform.position = randomPos;
                item.SetItemData(resource.ResourceSO.ItemDataSO, resource.ResourceSO.ItemDataSO.MaxStack);
            }
            PoolManager.Instance.Factory<Resource>().Push(resource);
            Debug.Log("아이템 스폰");
        }
        private void OnDisable()
        {
            resource.CurrentHp.OnDead -= SpawnItem;
        }
    }
}