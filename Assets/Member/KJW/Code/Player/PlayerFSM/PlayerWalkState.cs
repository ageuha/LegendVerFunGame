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
            _player.MoveCompo.SetMove(_player.InputReader.Dir);
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}