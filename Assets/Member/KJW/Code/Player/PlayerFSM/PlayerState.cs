namespace Member.KJW.Code.Player.PlayerFSM
{
    public abstract class PlayerState
    {
        protected Member.KJW.Code.Player.Player _player;
        protected PlayerStateMachine _stateMachine;

        protected PlayerState(Member.KJW.Code.Player.Player player, PlayerStateMachine stateMachine)
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