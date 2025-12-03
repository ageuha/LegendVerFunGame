namespace Code.UI.Setting.Interfaces {
    public interface ISettingValueHandler<in T> where T : struct {
        void OnValueChanged(T prev, T current);
    }
}