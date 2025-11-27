using KJW.Code.Player;
using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Item
{    
    public class ItemObjectTrigger : MonoBehaviour
    {
        private IPickable m_itemObject;

        private void Awake()
        {
            m_itemObject = GetComponentInParent<IPickable>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<Player>(out Player player))
            {
                m_itemObject.PickUp();
            }
        }
    }
}
