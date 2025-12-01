using UnityEngine;

public class BoatMovement : MonoBehaviour
{
        private BoatSOScript boatSO;
        private Rigidbody2D _rigid;
        public Vector2 MoveDir {get; private set;}
        private float _currentVelocity;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            boatSO = GetComponent<Boat>().BoatSO;
        }

        public void SetMove(Vector2 dir)
        {
            MoveDir = dir;
            _currentVelocity = CalculateSpeed(MoveDir);
        }

        private void Move()
        {
            _rigid.linearVelocity = MoveDir * _currentVelocity;
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
