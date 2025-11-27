namespace Code.UI {
    public interface IUIElement<in T> {
        void EnableFor(T item);
        void Disable();
    }
}