using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerRollState : PlayerState
    {
        public PlayerRollState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.MoveCompo.RbCompo.linearVelocity = Vector2.zero;
        }

        public override void Update()
        {
            base.Update();
            _player.MoveCompo.RbCompo.linearVelocity = _player.MoveCompo.StandDir * _player.RollingData.RollSpeed;
        }

        public override void Exit()
        {
            base.Exit();
            _player.MoveCompo.RbCompo.linearVelocity = Vector2.zero;
        }
    }
}