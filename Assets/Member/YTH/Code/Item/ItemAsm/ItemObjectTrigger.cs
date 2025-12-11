using System;
using Code.Core.GlobalSO;
using UnityEngine;

namespace Member.YTH.Code.Item
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        public event Action<Transform> Trigger;
        [SerializeField] private TagHandleSO targetTag;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(targetTag))
            {
                Trigger?.Invoke(collision.transform);
            }
        }
    }
}
