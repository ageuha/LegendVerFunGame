using System;
using Member.KJW.Code.Player;
using UnityEngine;

namespace YTH.Code.Item
{
    public class ItemObjectTrigger : MonoBehaviour
    {
        public Action<Player> Trigger;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Trigger?.Invoke(player);
            }
        }
    }
}
