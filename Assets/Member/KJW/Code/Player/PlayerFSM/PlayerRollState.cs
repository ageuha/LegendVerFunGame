using Member.KJW.Code.Enum;
using UnityEngine;

namespace Member.KJW.Code.Player.PlayerFSM
{
    public class PlayerRollState : PlayerState
    {
        private float _timer;
        
        public PlayerRollState(Member.KJW.Code.Player.Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
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

            if (!_player.IsRolling)
            {
                _stateMachine.UpdateState(PlayerStateType.Idle);
                _player.EndRoll();
                return;
            }
            
            _timer += Time.deltaTime;
            if (_timer <= _player.RollingData.RollTime + _player.RollingData.AfterDelayTime)
            {
                if (_timer >= _player.RollingData.RollTime)
                {
                    _player.MoveCompo.StopMove();
                }
            }
            else
            {
                _stateMachine.UpdateState(PlayerStateType.Idle);
                _player.EndRoll();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}