using System;
using System.Collections.Generic;
using Code.Core.Pool;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Thrower : MonoBehaviour
    {
        public void Throw(DamageInfo damageInfo, Sprite sprite, Vector2 dir, float speed)
        {
            Throwable th = PoolManager.Instance.Factory<Throwable>().Pop();
            th.Init(damageInfo, sprite, speed).Throw(dir);
        }
    }
}