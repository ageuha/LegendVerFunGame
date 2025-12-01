using KJW.Code.Player;
using UnityEngine;

public class BoatTrigger : MonoBehaviour
{
    private IRideable rideable;
    void Awake()
    {
        rideable = GetComponentInParent<IRideable>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
           rideable.SetNear(true,collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
           rideable.SetNear(false,null);
    }
}