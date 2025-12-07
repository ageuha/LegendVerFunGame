using Code.Core.Pool;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class PoolTest : MonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;
        public void OnPopFromPool()
        {
        }

        public void OnReturnToPool()
        {
        }
    }
}