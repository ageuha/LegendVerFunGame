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
            _playerStateMachine.CurrentState.Update();
            
            if (_playerStateMachine.CurrentStateType == PlayerStateType.Roll) return;
            
            if (_player.MoveCompo.MoveDir == Vector2.zero)
            {
                if (_playerStateMachine.CurrentStateType != PlayerStateType.Idle)
                {
                    _playerStateMachine.UpdateState(PlayerStateType.Idle);
                    return;
                }
                return;
            }
            
            if (_playerStateMachine.CurrentStateType == PlayerStateType.Walk) return;
            
            _playerStateMachine.UpdateState(PlayerStateType.Walk);
        }
    }
}