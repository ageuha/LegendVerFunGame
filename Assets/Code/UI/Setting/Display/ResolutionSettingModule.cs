using System.Collections.Generic;
using Code.UI.Setting.BaseClass;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class ResolutionSettingModule : DropdownSettingModule<Resolution> {
        private Resolution[] _resolutions;

        public override void SetSettingValue(Resolution value) {
            SettingValue.Value = value;
        }

        protected override void AfterAwake() {
            base.AfterAwake();
            SettingValue.Value = new Resolution
                { height = SettingSaveManager.Instance.Resolution.y, width = SettingSaveManager.Instance.Resolution.x };
            _resolutions = Screen.resolutions;
            dropdown.ClearOptions();
            HashSet<string> options = new HashSet<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++) {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.width &&
                    _resolutions[i].height == Screen.height) {
                    currentResolutionIndex = i;
                }
            }

            dropdown.AddOptions(new List<string>(options));
            dropdown.value = currentResolutionIndex;
            dropdown.RefreshShownValue();
        }

        protected override void OnDropdownValueChanged(int index) {
            base.OnDropdownValueChanged(index);
            SettingValue.Value = _resolutions[index];
        }
    }
}