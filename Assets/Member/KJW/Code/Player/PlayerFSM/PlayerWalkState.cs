using Member.KJW.Code.Enum;
using UnityEngine;

namespace Member.KJW.Code.Player.PlayerFSM
{
    public class PlayerWalkState : PlayerState
    {
        public PlayerWalkState(Member.KJW.Code.Player.Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            if (_player.InputReader.Dir == Vector2.zero)
            {
                _stateMachine.UpdateState(PlayerStateType.Idle);
                return;
            }

            if (_player.IsRolling)
            {
                _stateMachine.UpdateState(PlayerStateType.Roll);
                return;
            }
            
            _player.MoveCompo.SetMove(_player.InputReader.Dir);
            
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}