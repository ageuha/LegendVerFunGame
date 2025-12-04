using System.Collections.Generic;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Thrower : MonoBehaviour
    {
        [SerializeField] private Transform throwPos;
        [SerializeField] private GameObject throwPrefab;

        public void Throw(DamageInfo damageInfo, Vector2 dir, float throwSpeed)
        {
            GameObject go = Instantiate(throwPrefab, throwPos.position, throwPos.rotation);
            go.GetComponent<Throwable>().Init(damageInfo).Throw(dir, throwSpeed);
        }
    }
}