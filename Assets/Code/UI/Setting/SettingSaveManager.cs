using System.Collections.Generic;
using Code.Core.Utility;
using Code.SaveSystem;
using Code.UI.Setting.Enums;
using UnityEngine;

namespace Code.UI.Setting {
    public class SettingSaveManager : MonoSingleton<SettingSaveManager> {
        private Dictionary<SettingType, float> _floatData;
        public Vector2Int Resolution { get; set; }
        public bool IsFullscreen { get; set; }
        public bool VSync { get; set; }
        public int TargetFrame { get; set; }

        private SaveManagerBase<SettingSaveData> _saveManager;
        private SettingSaveData _saveData;

        protected override void Awake() {
            base.Awake();
            _saveManager = new JsonSaveManager<SettingSaveData>("SettingSaveData.json");
            LoadSetting();
            DontDestroyOnLoad(this);
        }

        private void OnApplicationQuit() {
            SaveSetting();
        }

        private void LoadSetting() {
            _saveData.LoadSaveData(_saveManager, capacity: 6);
            _floatData = _saveData.floatSetting.ToDictionary();
            Resolution = _saveData.resolution;
            IsFullscreen = _saveData.fullscreen;
            TargetFrame = _saveData.targetFrame;
            VSync = _saveData.vSync;
        }

        public void SaveSetting() {
            _saveData.floatSetting = new FloatSettingDict(_floatData);
            _saveData.resolution = Resolution;
            _saveData.fullscreen = IsFullscreen;
            _saveData.vSync = VSync;
            _saveData.targetFrame = TargetFrame;
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