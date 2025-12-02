using Member.KJW.Code.Enum;
using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            if (_player.InputReader.Dir != Vector2.zero)
            {
                _stateMachine.UpdateState(PlayerStateType.Walk);
                return;
            }
            
            if (_player.IsRolling)
            {
                _stateMachine.UpdateState(PlayerStateType.Roll);
                return;
            }
            
            _player.MoveCompo.SetMove(Vector2.zero);
            
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}