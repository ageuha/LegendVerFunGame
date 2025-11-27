using _02._Member.JJW._01_Code.Interface;

namespace _02._Member.JJW._01_Code.Enemy
{
    public class StateMachine<T>
    {
        private T _sender;
        
        public IState<T>  CurrentState { get;set; }

        public StateMachine(T sender, IState<T> state)
        {
            _sender = sender;
            CurrentState = state;
            SetState(state);
        }

        private void SetState(IState<T> state)
        {
            if(CurrentState != null)
                CurrentState.Exit(_sender);
            
            CurrentState = state; 
        }
    }
}