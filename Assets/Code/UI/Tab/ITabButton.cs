namespace Code.UI.Tab {
    public interface ITabButton {
        void ActivateTab();
        void DeactivateTab();
        int Index { get; }
    }
}