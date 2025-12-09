using System.Collections.Generic;
using Code.UI.Elements;
using Code.UI.Setting.BaseClass;
using TMPro;
using UnityEngine;

namespace Code.UI.Setting.Volume {
    public class VolumeSettingModule : ButtonSettingModule<float> {
        [SerializeField] private List<ToggleElement> elements;
        [SerializeField] private TextMeshProUGUI tmp;

        private sbyte _idx;

        protected override void AfterAwake() {
            base.AfterAwake();
            // _idx = 0; // 나중에 세이브 시스템 구축하면 값 가져와야 함.
            if (SettingSaveManager.Instance.TryGetFloat(SettingType, out var value)) {
                // 그래서 가져옴.
                SetIndexByFloat(value);
            }
            else {
                _idx = 0;
            }

            AfterInteract();
            InitializeElements();
        }

        public override void SetSettingValue(float value) {
            if (!SetIndexByFloat(value)) return;
            SettingValue.Value = value;

            AfterInteract();
            InitializeElements();
        }

        private bool SetIndexByFloat(float value) {
            sbyte temp = (sbyte)Mathf.RoundToInt(value * elements.Count - 1);
            if (temp == _idx) return false;
            _idx = temp;
            return true;
        }

        private void InitializeElements() {
            // 이름 비직관적. 초기화에 국한된 메서드 아님
            for (int i = 0; i < elements.Count; i++) {
                elements[i].EnableFor(i <= _idx);
            }
        }

        private void AfterInteract() {
            leftButton.interactable = _idx > -1;
            rightButton.interactable = _idx < elements.Count - 1;
            float percentage = (_idx + 1) / (float)elements.Count;
            SettingValue.Value = percentage;
            percentage *= 100f;
            tmp.text = $"{percentage}%";
        }

        protected override void OnLeftButtonClicked() {
            if (!leftButton.interactable) return;
            base.OnLeftButtonClicked();
            elements[_idx--].EnableFor(false);
            AfterInteract();
        }

        protected override void OnRightButtonClicked() {
            if (!rightButton.interactable) return;
            base.OnRightButtonClicked();
            elements[++_idx].EnableFor(true);

            AfterInteract();
        }
    }
}