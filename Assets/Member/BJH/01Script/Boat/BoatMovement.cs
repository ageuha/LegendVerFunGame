using UnityEngine;

namespace Member.BJH._01Script.Boat
{
    public class BoatMovement : MonoBehaviour
    {
        private BoatSOScript boatSO;
        private Rigidbody2D _rigid;
        private Vector2 _moveDir;
        private float _currentVelocity = 0;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            boatSO = GetComponent<Boat>().BoatSO;
        }

        public void SetMove(Vector2 dir, bool isMousePressed)
        {
            if (dir.sqrMagnitude <= 1)
            {
                _rigid.linearVelocity = Vector2.zero;
            }
            else
            {
                _moveDir = dir.normalized;
                _currentVelocity = CalculateSpeed(isMousePressed);
            }
        }
        public void StopMove()
        {
            _rigid.linearVelocity = Vector2.zero;
            _moveDir = Vector2.zero;
            _currentVelocity = 0;
        }

        private void Move()
        {
            _rigid.linearVelocity = _moveDir * _currentVelocity;
        }

        public float CalculateSpeed(bool isMousePressed)
        {
            if (isMousePressed)
            {
                _currentVelocity += boatSO.Acceleration * Time.deltaTime;
            }
            else
            {
                _currentVelocity -= boatSO.Deacceleration * Time.deltaTime;
            }
            return Mathf.Clamp(_currentVelocity, 0, boatSO.MaxSpeed);
        }

        private void FixedUpdate()
        {
            Move();
        }
    }
}