using UnityEngine;

public class BoatMovement : MonoBehaviour
{
        private BoatSOScript boatSO;
        private Rigidbody2D _rigid;
        private Vector2 _moveDir;
        private float _currentVelocity;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            boatSO = GetComponent<Boat>().BoatSO;
        }

        public void SetMove(Vector2 dir)
        {
            _moveDir = dir;
            _currentVelocity = CalculateSpeed(_moveDir);
        }

        private void Move()
        {
            _rigid.linearVelocity = _moveDir * _currentVelocity;
        }

        private float CalculateSpeed(Vector2 moveDir)
        {
            if (moveDir.sqrMagnitude > 0)
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
