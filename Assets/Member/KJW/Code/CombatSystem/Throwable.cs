using System;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Throwable : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;

        private void Reset()
        {
            rb ??= GetComponent<Rigidbody2D>();
        }

        public void Throw(Vector2 dir, float speed)
        {
            rb.linearVelocity = dir * speed;
        }
    }
}