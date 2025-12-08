using System.Collections.Generic;
using Code.Core.Utility;
using Code.SaveSystem;
using Code.UI.Setting.Enums;
using UnityEngine;

namespace Code.UI.Setting {
    public class SettingSaveManager : Singleton<SettingSaveManager> {
        private Dictionary<SettingType, float> _floatData;
        public Vector2Int Resolution { get; set; }
        public bool IsFullscreen { get; set; }

        private readonly SaveManagerBase<SettingSaveData> _saveManager;
        private SettingSaveData _saveData;

        public SettingSaveManager() {
            _saveManager = new JsonSaveManager<SettingSaveData>("SettingSaveData.json");
            LoadSetting();
        }

        private void LoadSetting() {
            _saveData.LoadSaveData(_saveManager, capacity: 6);
            _floatData = _saveData.floatSetting.ToDictionary();
            Resolution = _saveData.resolution;
            IsFullscreen = _saveData.fullscreen;
        }

        public void SaveSetting() {
            _saveData.floatSetting = new FloatSettingDict(_floatData);
            _saveData.resolution = Resolution;
            _saveData.fullscreen = IsFullscreen;
            _saveManager.SaveToFile(_saveData);
        }

        public void SetFloat(SettingType type, float value) {
            _floatData[type] = value;
        }

        public bool TryGetFloat(SettingType type, out float value) {
            return _floatData.TryGetValue(type, out value);
        }
    }
}