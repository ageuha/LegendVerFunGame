using System;
using Code.Core.Pool;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            PoolManager.Instance.Factory<PoolTest>().Pop();
        }
    }
}