using Member.KJW.Code.Enum;
using UnityEngine;

namespace Member.KJW.Code.Player.PlayerFSM
{
    public class PlayerBrain : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        private Member.KJW.Code.Player.Player _player;

        private void Awake()
        {
            _playerStateMachine = new PlayerStateMachine(this);
            _player = GetComponent<Member.KJW.Code.Player.Player>();
            
            _playerStateMachine.AddState(PlayerStateType.Idle, new PlayerIdleState(_player, _playerStateMachine));
            _playerStateMachine.AddState(PlayerStateType.Walk, new PlayerWalkState(_player, _playerStateMachine));
            _playerStateMachine.AddState(PlayerStateType.Roll, new PlayerRollState(_player, _playerStateMachine));
        }

        private void Start()
        {
            _playerStateMachine.Init(PlayerStateType.Idle);
        }

        private void Update()
        {
            _playerStateMachine.CurrentState.Update();
        }
    }
}