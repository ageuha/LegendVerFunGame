using System;
using _01._SO.Move;
using UnityEngine;

namespace _02._Member.KJW.Code.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private MovementData moveData;

        private Rigidbody2D _rbCompo;
        private Vector2 _moveDir;
        private float _currentVelocity;
        private bool _isDashing;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void SetMove(Vector2 movementInput, bool isDashing)
        {
            _isDashing = isDashing;
            _moveDir = movementInput;
            _currentVelocity = CalculateSpeed(_moveDir);
        }

        private void Move()
        {
            _rbCompo.linearVelocity = _moveDir * (_currentVelocity * (_isDashing ? moveData.DashMultiplyValue : 1));
        }

        private float CalculateSpeed(Vector2 moveDir)
        {
            if(moveDir.sqrMagnitude > 0)  //가속
            {
                _currentVelocity += moveData.Acceleration * Time.deltaTime;
            }
            else  //감속
            {
                _currentVelocity -= moveData.Deacceleration * Time.deltaTime;
            }
            return Mathf.Clamp(_currentVelocity, 0, moveData.MaxSpeed);
        }

        private void FixedUpdate()
        {
            Move();
        }

    }
}
