using System;
using Member.KJW.Code.Enum;
using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerBrain : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        private Player _player;

        private void Awake()
        {
            _playerStateMachine = new PlayerStateMachine(this);
            _player = GetComponent<Player>();
            
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
            if (_player.InputCompo.MoveDir != Vector2.zero)
            {
                _playerStateMachine.UpdateState(PlayerStateType.Walk);
                return;
            }
            else
            {
                _playerStateMachine.UpdateState(PlayerStateType.Idle);
                return;
            }
            _playerStateMachine.CurrentState.Update();
        }
    }
}