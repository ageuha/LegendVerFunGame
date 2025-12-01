using Member.KJW.Code.Enum;
using UnityEngine;

namespace KJW.Code.Player
{
    public class PlayerRollState : PlayerState
    {
        private float _timer;
        
        public PlayerRollState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _player.MoveCompo.StopMove();
            _timer = 0;
            _player.MoveCompo.AddForce(_player.StandDir * _player.RollingData.RollPower);
        }

        public override void Update()
        {
            base.Update();
            if (_timer <= _player.RollingData.RollTime)
                _timer += Time.deltaTime;
            else
            {
                _stateMachine.UpdateState(PlayerStateType.Idle);
                _player.IsRolling = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}