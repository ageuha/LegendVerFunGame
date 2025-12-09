using System;
using Code.SaveSystem;
using UnityEngine;

namespace Code.UI.Setting {
    [Serializable]
    public struct SettingSaveData {
        public FloatSettingDict floatSetting;
        public Vector2Int resolution;
        public bool fullscreen;
        public bool vSync;
        public int targetFrame;

        public void LoadSaveData(ISaveManager<SettingSaveData> saveManager, int capacity = 8) {
            this = saveManager.LoadSaveData(); // 깊은 복사라 상관X (확실)
            floatSetting ??= new FloatSettingDict(8); // 세이브 없을 때 대비
            if (resolution == default)
                resolution = new Vector2Int(Screen.width, Screen.height);
        }
    }
}