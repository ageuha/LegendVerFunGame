using System;
using System.Collections;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class AgentMovement : MonoBehaviour
    {
        [field: SerializeField] public MovementData MoveData { get; private set; }

        private Rigidbody2D _rb;
        private Vector2 _moveDir;
        private float _currentVelocity;
        public bool Stop { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetMove(Vector2 dir)
        {
            Stop = false;
            _moveDir = dir;
            _currentVelocity = CalculateSpeed(_moveDir);
        }
        
        public void StopMove()
        {
            Stop = true;
            _moveDir = Vector2.zero;
        }

        private void Move()
        {
            _rb.linearVelocity = _moveDir * _currentVelocity;
        }

        public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rb.AddForce(force, mode);
        }

        private float CalculateSpeed(Vector2 moveDir)
        {
            if (moveDir.sqrMagnitude > 0)
            {
                _currentVelocity += MoveData.Acceleration * Time.deltaTime;
            }
            else
            {
                _currentVelocity -= MoveData.Deacceleration * Time.deltaTime;
            }
            return Mathf.Clamp(_currentVelocity, 0, MoveData.MaxSpeed);
        }

        private void FixedUpdate()
        {
            if (Stop) return;
            Move();
        }
    }
}