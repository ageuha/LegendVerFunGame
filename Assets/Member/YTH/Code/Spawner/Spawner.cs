using System.Collections.Generic;
using System.Diagnostics;
using Code.Core.Pool;
using Code.EntityScripts.BaseClass;
using Code.EntityScripts.ConcreteClass;
using Member.KJW.Code.Player;
using UnityEngine;

namespace YTH.Code.Spawner
{    
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private float distance;
        [SerializeField] private EntityType entityType;

        private float m_Time;

        private Player _player;
        private Player Player => _player ??= FindAnyObjectByType<Player>();


        private void Update()
        {
            m_Time += Time.deltaTime;

            if (m_Time >= time)
            {
                if (Vector2.Distance(Player.transform.position, transform.position) <= distance)
                {    
                    m_Time = 0;
                    
                    Entity entity = entityType switch
                    {
                        EntityType.Chicken => PoolManager.Instance.Factory<Chicken>().Pop(),
                        EntityType.Cow => PoolManager.Instance.Factory<Cow>().Pop(),
                        EntityType.Kiwi => PoolManager.Instance.Factory<Kiwi>().Pop(),
                        EntityType.Rabbit => PoolManager.Instance.Factory<Rabbit>().Pop(),
                        EntityType.Turtle => PoolManager.Instance.Factory<Turtle>().Pop(),
                        _ => null
                    };

                    entity.transform.position = transform.position;
                }

            }
        }

    }

    public enum EntityType
    {
        Chicken,
        Cow,
        Kiwi,
        Rabbit,
        Turtle
    }
}
