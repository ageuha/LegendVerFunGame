using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerWalkState : PlayerState
    {
        public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            _player.MoveCompo.SetMove(_player.InputCompo.MoveDir);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}