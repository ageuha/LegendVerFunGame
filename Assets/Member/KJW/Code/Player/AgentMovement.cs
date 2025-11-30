using System;
using System.Collections;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class AgentMovement : MonoBehaviour
    {
        [field: SerializeField] public MovementData MoveData { get; private set; }

        private Rigidbody2D _rbCompo;
        private Vector2 _moveDir;
        private Vector2 _standDir;
        private float _currentVelocity;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetMove(Vector2 dir)
        {
            _moveDir = dir;
            _standDir = _moveDir != Vector2.zero ? _moveDir : _standDir;
            _currentVelocity = CalculateSpeed(_moveDir);
        }

        private void Move()
        {
            _rbCompo.linearVelocity = _moveDir * _currentVelocity;
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
            Move();
        }
    }
}