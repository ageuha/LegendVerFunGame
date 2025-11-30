using UnityEngine;

namespace Member.KJW.Code.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInput _inputCompo;
        private PlayerMovement _moveCompo;

        private void Awake()
        {
            _inputCompo = GetComponent<PlayerInput>();
            _moveCompo = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _moveCompo.SetMove(_inputCompo.MoveDir, _inputCompo.IsDashing);
        }

    }
}