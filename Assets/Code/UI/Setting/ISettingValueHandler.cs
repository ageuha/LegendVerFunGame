namespace Code.UI.Setting {
    public interface ISettingValueHandler<in T> where T : struct {
        void OnValueChanged(T prev, T current);
    }
}