using System;
using UnityEngine;

namespace _02._Member.KJW.Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float moveSpeed;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            _rigidbody2D.linearVelocity = direction * moveSpeed;
        }
    }
}
