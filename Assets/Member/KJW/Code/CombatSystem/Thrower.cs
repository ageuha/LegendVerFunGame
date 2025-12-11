using System;
using System.Collections.Generic;
using Code.Core.Pool;
using UnityEngine;
using YTH.Code.Item;

namespace Member.KJW.Code.CombatSystem
{
    public class Thrower : MonoBehaviour
    {
        public void Throw(WeaponDataSO weaponData, Vector2 dir)
        {
            Throwable th = PoolManager.Instance.Factory<Throwable>().Pop();
            th.Init(weaponData).Throw(dir);
        }
    }
}