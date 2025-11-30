using System;
using System.Collections;
using KJW.Code.Data;
using UnityEngine;

namespace KJW.Code.Player
{
    public class AgentMovement : MonoBehaviour
    {
        [field: SerializeField] public MovementData MoveData { get; private set; }

        public Rigidbody2D RbCompo {get; private set;}
        public Vector2 MoveDir {get; private set;}
        public Vector2 StandDir {get; private set;}
        private float _currentVelocity;

        private void Awake()
        {
            RbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetMove(Vector2 dir)
        {
            MoveDir = dir;
            StandDir = MoveDir != Vector2.zero ? MoveDir : StandDir;
            _currentVelocity = CalculateSpeed(MoveDir);
        }

        private void Move()
        {
            RbCompo.linearVelocity = MoveDir * _currentVelocity;
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