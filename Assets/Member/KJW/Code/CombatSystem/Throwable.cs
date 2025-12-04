using System;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Throwable : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _rb  = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Init()
        {
            
        }

        public void Throw(Vector2 dir, float speed)
        {
            _rb.linearVelocity = dir * speed;
        }
    }
}