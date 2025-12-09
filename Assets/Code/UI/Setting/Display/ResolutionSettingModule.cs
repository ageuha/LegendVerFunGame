using System.Collections.Generic;
using Code.Core.Utility;
using Code.UI.Setting.BaseClass;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class ResolutionSettingModule : DropdownSettingModule<Resolution> {
        private List<Resolution> _resolutions;

        public override void SetSettingValue(Resolution value) {
            SettingValue.Value = value;
        }

        protected override void AfterAwake() {
            base.AfterAwake();

            SettingValue.Value = new Resolution {
                height = SettingSaveManager.Instance.Resolution.y,
                width = SettingSaveManager.Instance.Resolution.x
            };

            var allResolutions = Screen.resolutions;
            _resolutions = new List<Resolution>(allResolutions.Length);
            var optionStrings = new List<string>(allResolutions.Length);

            int currentResolutionIndex = 0;

            foreach (var r in allResolutions) {
                string option = $"{r.width} x {r.height}";

                bool exists = false;
                foreach (var res in _resolutions) {
                    if (res.width == r.width &&
                        res.height == r.height) {
                        exists = true;
                        break;
                    }
                }

                if (exists) continue;

                _resolutions.Add(r);
                optionStrings.Add(option);

                if (r.width == Screen.width && r.height == Screen.height) {
                    currentResolutionIndex = _resolutions.Count - 1;
                }
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(optionStrings);
            dropdown.value = currentResolutionIndex;
            dropdown.RefreshShownValue();
        }

        protected override void OnDropdownValueChanged(int index) {
            base.OnDropdownValueChanged(index);
            SettingValue.Value = _resolutions[index];
        }
    }
}