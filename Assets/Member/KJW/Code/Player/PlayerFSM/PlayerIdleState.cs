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
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}