using System.Collections.Generic;
using Code.Core.Utility;
using Code.SaveSystem;

namespace Code.UI.Setting {
    public class SettingSaveManager : Singleton<SettingSaveManager> {
        private Dictionary<SettingType, float> _floatData;

        private SaveManagerBase<SettingSaveData> _saveManager;
        private SettingSaveData _saveData;

        public SettingSaveManager() {
            _saveManager = new JsonSaveManager<SettingSaveData>("SettingSaveData.json");
            _floatData = new Dictionary<SettingType, float>(6);
            _saveData = _saveManager.LoadSaveData();
        }

        public void SetFloat(SettingType type, float value) {
            _floatData[type] = value;
        }

        public bool TryGetFloat(SettingType type, out float value) {
            return _floatData.TryGetValue(type, out value);
        }
    }
}