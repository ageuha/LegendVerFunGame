using System.Collections.Generic;
using Code.Core.Utility;
using Member.KJW.Code.Enum;

namespace KJW.Code.Player
{
    public class PlayerStateMachine
    {
        private Dictionary<PlayerStateType, PlayerState> _stateDict = new();

        public PlayerState CurrentState {get; private set;}
        private PlayerBrain _player;

        public PlayerStateMachine(PlayerBrain player)
        {
            _player = player;
        }

        public void AddState(PlayerStateType stateType, PlayerState state)
        {
            _stateDict.Add(stateType, state);
        }

        public void UpdateState(PlayerStateType stateType)
        {
            CurrentState?.Exit();
            CurrentState = _stateDict[stateType];
            CurrentState?.Enter();
        }

        public void Init(PlayerStateType stateType)
        {
            CurrentState = _stateDict[stateType];
            CurrentState?.Enter();
        }
    }
}