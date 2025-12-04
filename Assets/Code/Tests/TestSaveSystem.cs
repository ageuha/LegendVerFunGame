using Code.UI.Setting;
using UnityEngine;

namespace Code.Tests {
    public class TestSaveSystem : MonoBehaviour {
        [ContextMenu("Save")]
        public void Save() => SettingSaveManager.Instance.SaveSetting();
    }
}