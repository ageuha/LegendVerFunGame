namespace KJW.Code.Player
{
    public abstract class PlayerState
    {
        protected Player _player;
        protected PlayerStateMachine _stateMachine;

        protected PlayerState(Player player, PlayerStateMachine stateMachine)
        {
            _player = player;
            _stateMachine = stateMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Exit()
        {
            
        }
    }
}