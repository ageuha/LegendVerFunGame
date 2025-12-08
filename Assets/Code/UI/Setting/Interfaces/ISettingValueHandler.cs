using Code.UI.Setting.Enums;

namespace Code.UI.Setting.Interfaces {
    public interface ISettingValueHandler<in T> where T : struct {
        void Initialize(SettingType settingType);
        void OnValueChanged(T prev, T current);
    }
}