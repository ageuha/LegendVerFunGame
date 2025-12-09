using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using Member.KJW.Code.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.UI.Setting.KeyBinding {
    public class KeyHandler : MonoBehaviour, ISettingValueHandler<Key> {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Key test;
        

        private SettingType _settingType;
        private InputAction _action;

        public void Initialize(SettingType settingType) {
            _settingType = settingType;
        }

        public void OnValueChanged(Key prev, Key current) {
        }
    }
}