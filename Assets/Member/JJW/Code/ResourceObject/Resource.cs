using Code.EntityScripts;
using Member.JJW.Code.Interface;
using UnityEngine;
using YTH.Code.Interface;
using YTH.Code.Item;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.ResourceObject
{
    public class Resource : MonoBehaviour,IInteractable<float>
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private ItemDataSO itemDataSO;
        [SerializeField] private float hp = 100;
        [SerializeField] private int spawnItemAmount =1;
        [SerializeField] private float itemSpawnRadius = 1f;
        
        private HealthSystem _hp;

        private void Awake()
        {
            _hp = GetComponent<HealthSystem>();
            _hp.Initialize(hp);
            _hp.OnDead += SpawnItem;
        }

        private void SpawnItem()
        {
            for (int i = 0; i < spawnItemAmount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                GameObject item = Instantiate(itemPrefab,randomPos,Quaternion.identity);
                if (item.TryGetComponent<ItemObject>(out ItemObject itemObject))
                {
                    itemObject.SetItemData(itemDataSO,spawnItemAmount);
                }
            }
            Destroy(gameObject);
        }

        public void Interaction(float value)
        {
            _hp.ApplyDamage(value);
        }

        private void OnDestroy()
        {
            _hp.OnDead -= SpawnItem;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, itemSpawnRadius);
        }
    }
}