using System.Collections.Generic;
using Code.Core.Utility;
using Code.SaveSystem;

namespace Code.UI.Setting {
    public class SettingSaveManager : Singleton<SettingSaveManager> {
        private Dictionary<SettingType, float> _floatData;

        private readonly SaveManagerBase<SettingSaveData> _saveManager;
        private SettingSaveData _saveData;

        public SettingSaveManager() {
            _saveManager = new JsonSaveManager<SettingSaveData>("SettingSaveData.json");
            LoadSetting();
        }

        private void LoadSetting() {
            _saveData = _saveManager.LoadSaveData();
            _floatData = _saveData.floatSetting.ToDictionary();
        }

        public void SaveSetting() {
            // _saveData.floatSetting = new FloatSettingDict(_floatData);
            _saveManager.SaveToJson(_saveData);
        }

        public void SetFloat(SettingType type, float value) {
            _floatData[type] = value;
        }

        public bool TryGetFloat(SettingType type, out float value) {
            return _floatData.TryGetValue(type, out value);
        }
    }
}