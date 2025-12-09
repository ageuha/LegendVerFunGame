using System.Collections.Generic;
using Code.UI.Setting.BaseClass;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class FPSSettingModule : DropdownSettingModule<int> {
        [SerializeField] private List<int> frames = new();

        public override void SetSettingValue(int value) {
            SettingValue.Value = value;
        }

        protected override void AfterAwake() {
            base.AfterAwake();

            SettingValue.Value = SettingSaveManager.Instance.TargetFrame;

            int currentFrameIndex = 0;
            var optionStrings = new List<string>(frames.Count);

            foreach (var f in frames) {
                var option = f != -1 ? $"{f}FPS" : "무제한";

                optionStrings.Add(option);


                if (f == Application.targetFrameRate) {
                    currentFrameIndex = frames.IndexOf(f);
                }
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(optionStrings);
            dropdown.value = currentFrameIndex;
            dropdown.RefreshShownValue();
        }

        protected override void OnDropdownValueChanged(int index) {
            base.OnDropdownValueChanged(index);
            SettingValue.Value = frames[index];
        }
    }
}