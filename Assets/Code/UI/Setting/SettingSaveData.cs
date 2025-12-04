using System;
using Code.SaveSystem;

namespace Code.UI.Setting {
    [Serializable]
    public struct SettingSaveData {
        public FloatSettingDict floatSetting;

        public void LoadSaveData(ISaveManager<SettingSaveData> saveManager, int capacity = 8) {
            this = saveManager.LoadSaveData(); // 깊은 복사라 상관X (확실)
            floatSetting ??= new FloatSettingDict(8);
        }
    }
}