namespace _02._Member.JJW._01_Code.Interface
{
    public interface IState<T>
    {
        public void Enter(T sender);
        public void Exit(T sender);
        public void Update(T sender);
    }
}