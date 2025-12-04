using System;
using Member.JJW.Code.Interface;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.InteractableObject
{
    public class Tree :  InteractableObject
    {
        [SerializeField] private CollectableObject.CollectableObject collectableObject;
        [SerializeField] private int spawnItemCount =1;
        [SerializeField] private float itemSpawnRadius = 1f;
        [SerializeField] private int hp;
        public override int Hp
        {
            get => hp;
            set
            {
                Debug.Log($"Hp: {value}");
                hp = value;
                if (value <= 0)
                {
                    SpawnItem();
                }
            }
        }

        public override void Interaction(int decreaseAmount)
        {
            Hp -= decreaseAmount;
        }

        public override void SpawnItem()
        {
            for (int i = 0; i < spawnItemCount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                CollectableObject.CollectableObject collectableObj = Instantiate(collectableObject, randomPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, itemSpawnRadius);
        }
    }
}