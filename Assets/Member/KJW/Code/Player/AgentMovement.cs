using Member.KJW.Code.Data;
using UnityEngine;

namespace Member.KJW.Code.Player
{
    public class AgentMovement : MonoBehaviour
    {
        [field: SerializeField] public MovementData MoveData { get; private set; }

        private Rigidbody2D _rb;
        private Vector2 _moveDir;
        private float _currentVelocity;
        public bool IsStop { get; private set; }
        public float SpeedMultiplyValue { get; private set; } = 1;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetMove(Vector2 dir)
        {
            if (IsStop) return;

            _moveDir = dir;
            _currentVelocity = CalculateSpeed(_moveDir);
        }

        public void RestartMove()
        {
            IsStop = false;
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
        
        public void StopMove()
        {
            IsStop = true;
            _moveDir = Vector2.zero;
            _rb.linearVelocity = _moveDir;
            _currentVelocity = 0;
        }

        public void SetMultiValue(float value)
        {
            SpeedMultiplyValue = value;
        }

        public void SetKinematic(bool isKinematic)
        {
            _rb.bodyType = isKinematic ?  RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
        }

        private void Move()
        {
            _rb.linearVelocity = _moveDir * (_currentVelocity * SpeedMultiplyValue);
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
            if (IsStop) return;
            Move();
        }
    }
}